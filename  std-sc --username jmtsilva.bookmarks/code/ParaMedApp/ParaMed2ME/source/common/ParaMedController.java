package common;

import interfaces.IParaMedModel;
import interfaces.IParaMedView;

public abstract class ParaMedController {
	protected IParaMedView view;
	protected IParaMedModel model;
	
	public void terminateApplication() {
		view.terminateApplication();
	}
}
