package TaggerModule;

import java.util.Enumeration;
import javax.comm.CommPortIdentifier;
import javax.comm.SerialPort;
import java.util.ArrayList;
import TaggerModule.NotImplementedException;

public class Tagger implements TaggerInterface {
	
	// SerialPort
	protected SerialPort serialPort = null;
	// String that holds the response from the reader
	protected String response;
	// String that is used to build the response
	protected StringBuilder readedData;
	// Set time to wait for response from the reader
	protected long timeout = 5000;
	// Set time to wait for a card be in range
	protected long selectTimeout = 15000;
	// Indicates whenever the connection was established
	protected boolean connected = false;
	// Indicates whenever a card is selected
	protected boolean cardSelected = false;
	// Set the max size to store on tag
	protected int maxsize;
	
	public Tagger()
	{
	}
	
	@SuppressWarnings("unchecked")
	public ArrayList<String> getAvaibleCOMPorts() throws TaggerException {
		Enumeration portEnum = CommPortIdentifier.getPortIdentifiers();
		
		ArrayList<String> ports = new ArrayList<String>();
		while ( portEnum.hasMoreElements() ) {
			CommPortIdentifier portId;
			
			portId = (CommPortIdentifier)portEnum.nextElement();
			
			if ( portId.getPortType()==CommPortIdentifier.PORT_SERIAL )
				ports.add( portId.getName() );
		}
		return ports;
	}

	@Override
	public boolean connect(String COM) throws TaggerException, NotImplementedException {
		throw new NotImplementedException();
	}

	@Override
	public boolean disconnect() throws TaggerException, NotImplementedException {
		throw new NotImplementedException();
	}

	@Override
	public String read() throws TaggerException, NotImplementedException {
		throw new NotImplementedException();
	}

	@Override
	public boolean write(String csv) throws TaggerException, NotImplementedException {
		throw new NotImplementedException();
	}

	@Override
	public String version() throws TaggerException, NotImplementedException {
		// TODO Auto-generated method stub
		return null;
	}

}
