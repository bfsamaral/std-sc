package proxy;

import interfaces.IParaMedView;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import common.ParaMedController;

public class ParaMedControllerProxy2SE  extends ParaMedController
	implements ActionListener {

	public ParaMedControllerProxy2SE(IParaMedView _view) {
		view = _view;
	}
	
	public void actionPerformed(ActionEvent action) {
		if(action.getActionCommand() == "Click2close") {
			terminateApplication();
		}
		if(action.getActionCommand() == "Exit") {
			terminateApplication();		}
	}

}
