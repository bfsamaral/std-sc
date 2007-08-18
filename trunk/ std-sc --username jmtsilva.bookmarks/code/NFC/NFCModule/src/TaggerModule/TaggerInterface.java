package TaggerModule;

import java.util.ArrayList;

public interface TaggerInterface {
	public ArrayList<String> getAvaibleCOMPorts() throws TaggerException, NotImplementedException;
	public boolean connect( String COM ) throws TaggerException, NotImplementedException;
	public boolean write( String csv ) throws TaggerException, NotImplementedException;
	public String read() throws TaggerException, NotImplementedException;
	public boolean disconnect() throws TaggerException, NotImplementedException;
	public String version() throws TaggerException, NotImplementedException;
}
