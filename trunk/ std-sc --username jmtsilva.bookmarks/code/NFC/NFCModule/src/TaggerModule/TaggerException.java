package TaggerModule;

@SuppressWarnings({ "serial" })
public class TaggerException extends Exception {
	TaggerException(String msg) {
		super(msg);
	}
	TaggerException() {
		this("");
	}
}
