package proxy;

import interfaces.IParaMedModel;
import interfaces.IParaMedView;

import javax.microedition.lcdui.Command;
import javax.microedition.lcdui.CommandListener;
import javax.microedition.lcdui.Displayable;

import common.ParaMedController;


public class ParaMedControllerProxy2ME extends ParaMedController 
	implements CommandListener{

	public ParaMedControllerProxy2ME(IParaMedView _view) {
		view = _view;
	}
	
	public void commandAction(Command command, Displayable arg1) {
		if (command.getCommandType() == 1)
		{
			terminateApplication();
		}	
	}
}
