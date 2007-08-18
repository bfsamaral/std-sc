package TaggerModule;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Enumeration;
import javax.comm.*;
import java.lang.StringBuilder;

/*
 * Class that manages all the communication with the NFC reader
 */
public class Arygon extends Tagger implements SerialPortEventListener, TaggerInterface {
	
	// Optional response from reader
	protected String response2;
	// Block where is write the size of the data stored on card
	protected String sizeBlock = "02";
	// Start block for writing content
	protected String baseBlock = "04";
	
	public Arygon() 
	{
		System.out.println("NFC Created");
		this.readedData = new StringBuilder();
	}

	@Override
	public boolean connect(String COM) throws TaggerException 
	{
		try {
			CommPortIdentifier portId;
			portId = CommPortIdentifier.getPortIdentifier( COM );

			this.serialPort = (SerialPort)portId.open(
				Thread.currentThread().getName(), 
				5000
			);

			this.serialPort.enableReceiveTimeout( 100 );
  
			this.serialPort.setSerialPortParams( 
				9600, 
				SerialPort.DATABITS_8, 
				SerialPort.STOPBITS_1, 
				SerialPort.PARITY_NONE
			);
  
			this.serialPort.addEventListener( this );
			this.serialPort.notifyOnDataAvailable(true);
			
			return this.connected = true;
		}
		catch( Exception exc) {
			throw new TaggerException( exc.getMessage() );
		}
	}

	@Override
	public boolean disconnect() throws TaggerException 
	{
		try {
			this.serialPort.close();
			this.connected = false;
			return true;
		}
		catch( Exception exc ) {
			throw new TaggerException(exc.getMessage());
		}
	}
	
	public String version() throws TaggerException
	{
		if ( !this.connected ) throw new TaggerException("Reader not connected");
		
		try {
			// Get version
			return this.versionCmd();
		}
		catch( Exception exc ) {
			this.disconnect();
			throw new TaggerException( exc.getMessage() );
		}
	}

	@Override
	public String read() throws TaggerException 
	{
		if ( !this.connected ) throw new TaggerException("Reader not connected");
		
		try {
			// Default is 16bytes
			StringBuilder data = new StringBuilder();
			
			// Select card
			this.selectCardCmd();
			
			if ( !this.cardSelected ) throw new TaggerException("No card selected");
			
			// Authenticate sector to put the data size
			this.loginCmd( this.sizeBlock );
			// Access to block 02 (sector 0) to read the size (bytes) of the store info
			String sizeStored = this.readCmd(this.sizeBlock);
			double size = Integer.parseInt(sizeStored,16);
			
			// Calculate the number of cycles
			int cycles = (int)Math.ceil(size/16);
			int block = Integer.parseInt(this.baseBlock);
			
			// Read slots to string (16bytes=16chars)
			for( int i=0; i<cycles; ++i )
			{
				// Authenticate the sector of the first block
				if ( i == 0 ) this.loginCmd(this.baseBlock);
				else {
					// Calculate the block to write
					block++;
					
					// If it's block is a trailer one. Jump to next block
					int sector = (int)Math.floor((double)block/4);
					int trailer = sector*4+3;
					if ( block == trailer ) block++;
					
					// Authenticate whenever is a new sector
					if ( block % 4 == 0 ) this.loginCmd(this.toHex(block));
				}
				
				String tmp = this.readCmd(this.toHex(block));
				for( int j=0; j<16; ++j ) {
					if ( i*16+j == (int)size ) break;
					int val = Integer.parseInt(tmp.substring(j*2,j*2+2), 16);
					char c = (char)val;
					data.append(c);
				}	
			}
			this.cardSelected = false;
			return data.toString();
		}
		catch( Exception exc ) {
			this.disconnect();
			throw new TaggerException( exc.getMessage() );
		}
	}

	@Override
	public boolean write(String csv) throws TaggerException 
	{
		if ( !this.connected ) throw new TaggerException("Reader not connected");
		
		try {
			// Select card
			this.selectCardCmd();
			
			if ( !this.cardSelected ) throw new TaggerException("No card selected");
			if ( csv.length() > this.maxsize ) throw new TaggerException("The string length must the lower or equal to 720");
			
			StringBuilder data;		
			
			// Calculate the number of cycles
			double length = csv.length();
			int cycles = (int)Math.ceil(length/16);
			int block = Integer.parseInt(this.baseBlock);
			
			// Authenticate sector to put the data size
			this.loginCmd( this.sizeBlock );		
			// Write data on size block
			String size = this.toHex((int)length);
			StringBuilder datasize = new StringBuilder();
			for( int i=0; i<(32-size.length()); ++i ) datasize.append("0");
			datasize.append(size);
			this.writeCmd( this.sizeBlock, datasize.toString() );
			
			// Break String in slots (16bytes=16chars)
			for( int i=0; i<cycles; ++i )
			{
				// Authenticate the sector of the first block
				if ( i == 0 ) this.loginCmd(this.baseBlock);
				else {
					// Calculate the block to write
					block++;
					
					// If it's block is a trailer one. Jump to next block
					int sector = (int)Math.floor((double)block/4);
					int trailer = sector*4+3;
					if ( block == trailer ) block++;
					
					// Authenticate whenever is a new sector
					if ( block % 4 == 0 ) this.loginCmd(this.toHex(block));
				}
				
				data = new StringBuilder(); 
				for( int j=i*16; j<i*16+16; ++j )
				{
					if ( j >= (int)length ) {
						data.append("00");
					} else {
						int ascii = (int)csv.charAt(j);
						String hex = this.toHex(ascii);
						data.append(hex);
					}
				}
				this.writeCmd(this.toHex(block), data.toString());			
			}
			this.cardSelected = false;
			
			return true;
		}
		catch( Exception exc ) {
			this.disconnect();
			throw new TaggerException( exc.getMessage() );
		}
	}
	
	protected String toHex( int val )
	{
		String tmp = Integer.toHexString(val);
		if ( tmp.length() % 2 != 0 ) return "0"+tmp;
		return tmp;
	}
	
	
	/*
	 * Command execution 
	 */
	
	protected String versionCmd() throws TaggerException, NotImplementedException {
		try {
			this.sendCmd("0av", false, this.timeout);
			if ( this.response.length() > 0 ) {
				String response = this.response.substring(6);
				String size = response.substring(0,2);
				String ucvariant = response.substring(2,4);
				int resp_size = Integer.parseInt(size, 16);
				return response.substring(4,2+resp_size);
			}
			else throw new TaggerException( "Cannot get version" );
		}
		catch( Exception exc ) {
			exc.printStackTrace();
			this.disconnect();
			throw new TaggerException( exc.getMessage() );
		}
	}
	
	// Sector is a block in the sector
	// Authentication is done with a key store in the EEPROM
	protected void selectCardCmd() throws TaggerException, IOException, InterruptedException
	{
		StringBuilder cmd = new StringBuilder();
		cmd.append("0s");
		
		/*
		this.cardSelected = true;
		this.maxsize = 720;
		System.out.println(cmd);
		*/
		
		// Use selectTimeout instead of timeout
		this.sendCmd(cmd.toString(), true, this.selectTimeout);
		
		String error = "";
		if ( this.response.length() > 0 && this.response2.length() > 0 ) {
			if ( this.response.equals("FF000000") && this.response2.length() == 30 ){
				// Check user data length
				if ( this.response2.substring(6,8).equals("16") ) {
					// Check card type
					if ( this.response2.substring(18,20).equals("08") || this.response2.substring(18,20).equals("88") ) 
					{
						this.cardSelected = true;
						this.maxsize = 720;
						return;
					}
					else if ( this.response2.substring(18,20).equals("18") ) 
					{
						this.cardSelected = true;
						this.maxsize = 1488;
						return;
					}
					else error = "Card not supported";
				}
				else error = "Card not supported";
			} else error = "Unknown response";
		} else error = "No response received";
		
		throw new TaggerException("Invalid write with error: " + error );
	}
	
	// Sector is a block in the sector
	// Authentication is done with a key store in the EEPROM
	protected void loginCmd( String block ) throws IOException, InterruptedException, TaggerException
	{
		StringBuilder cmd = new StringBuilder();
		cmd.append("0l");
		cmd.append(block);
		// Default EEPROM key B
		cmd.append("05");
		
		//System.out.println(cmd);
		
		this.sendCmd(cmd.toString(), true, this.timeout);
		
		String error = "";
		if ( this.response.length() > 0 && this.response2.length() > 0 ) {
			if ( this.response.equals("FF000000") && this.response2.substring(6,8).equals("04") ){
				String tmp = this.response2.substring(10, 12);
				if ( tmp.equals("00") ) return;
				else if ( tmp.equals("01") ) error = "Interrupt write overflow";
			} else error = "Unknown response";
		} else error = "No response received";
		
		throw new TaggerException("Invalid write with error: " + error );
	}
	
	protected void writeCmd( String block, String data ) throws IOException, InterruptedException, TaggerException
	{
		StringBuilder cmd = new StringBuilder();
		cmd.append("0wb");
		cmd.append(block);
		cmd.append(data);		
		
		//System.out.println(cmd);
		
		this.sendCmd(cmd.toString(), true, this.timeout);
		
		String error = "";
		if ( this.response.length() > 0 && this.response2.length() > 0 ) {
			if ( this.response.equals("FF000000") && this.response2.substring(6,8).equals("04") ){
				String tmp = this.response2.substring(10, 12);
				if ( tmp.equals("00") ) return;
				else if ( tmp.equals("01") ) error = "Interrupt write overflow";
			} else error = "Unknown response";
		} else error = "No response received";
		
		throw new TaggerException("Invalid write with error: " + error );
	}
	
	protected String readCmd( String block ) throws IOException, InterruptedException, TaggerException
	{
		StringBuilder cmd = new StringBuilder();
		cmd.append("0r");
		cmd.append(block);
		
		/*
		System.out.println(cmd);
		if ( block.equals("02") ) return "00000000000000000000000000000032";
		else if ( block.equals("04") ) return "44444444444444444444444444444444";
		else if ( block.equals("05") ) return "55555555555555555555555555555555";
		else if ( block.equals("06") ) return "66666666666666666666666666666666";
		else if ( block.equals("08") ) return "77770000000000000000000000000000";
		else return "";
		*/
		
		this.sendCmd(cmd.toString(), true, this.timeout);
		
		String error = "";
		if ( this.response.length() > 0 && this.response2.length() > 0 ) {
			if ( this.response2.substring(6,8).equals("24") )
			{
				String errorcode = this.response2.substring(10, 12);
				if ( errorcode.equals("00") ) return this.response2.substring(12);
				else if ( errorcode.equals("01") ) error = "Interrupt write overflow";
			} else error = "Unknown response";
		} else error = "No response received";
		
		throw new TaggerException("Invalid write with error: " + error );
	}
	
	protected synchronized void sendCmd( String cmd, boolean optionalresponse, long timeout ) throws IOException, InterruptedException
	{
		this.response = this.response2 = "";
		this.readedData = new StringBuilder();
		this.serialPort.getOutputStream().write(cmd.getBytes());
		this.wait(timeout);
		this.response = this.readedData.toString();
		this.readedData = new StringBuilder();
		if ( optionalresponse ) {
			this.wait(this.timeout);
			this.response2 = this.readedData.toString();
		}
	}

	public synchronized void serialEvent(SerialPortEvent arg0) {
		switch ( arg0.getEventType() ) {
	      case SerialPortEvent.DATA_AVAILABLE:
	    	  // TODO: Test how this work with long time and "beacon" reponses
	    	  //System.out.println( "data avaible" );
	          try {
		            int bytesAvailable = this.serialPort.getInputStream().available();
		            byte dataBytes[] = new byte [bytesAvailable];
		            this.serialPort.getInputStream().read(dataBytes, 0, bytesAvailable);
		            String data = new String(dataBytes);
		            if ( !data.equals("\r") && !data.equals("\n") ) this.readedData.append(data);
		            if ( data.equals("\n") ) this.notify(); 
	          }
	          catch (IOException ioEx) { ioEx.printStackTrace(); }
	          catch (Exception exc) { exc.printStackTrace(); }
	          break;
		}
	}	
}
