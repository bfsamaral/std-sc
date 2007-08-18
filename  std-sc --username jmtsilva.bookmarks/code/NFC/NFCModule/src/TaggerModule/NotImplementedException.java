package TaggerModule;

@SuppressWarnings("serial")
public class NotImplementedException extends Exception {
	NotImplementedException(String msg) {
		super(msg);
	}
	NotImplementedException() {
		this("");
	}
}
