import java.util.ArrayList;

import TaggerModule.*;

public class App {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		System.out.println("Starting test NFC Module");
				
		try {
			TaggerInterface reader = new Arygon();
			
			ArrayList list = reader.getAvaibleCOMPorts();
			for ( Object c:list ) {
				System.out.println(c);
			}
			
			reader.connect( "COM4" );
			
			System.out.println( reader.version() );
			
			/* Simulation
				System.out.println( "Write test" );
				reader.write("1=bruno,2=amaral,3=montijo,4=isel,5=22anos,6=teste");
				System.out.println( "Read test" );
				System.out.println( reader.read() );
			*/
			
			reader.write("1=bruno,2=amaral,3=montijo");
			
			System.out.println(reader.read());
			
			reader.disconnect();
			
		}
		catch( Exception exc ) {
			System.out.println(exc.getMessage());
		}
	}

}
