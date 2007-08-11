package paraMed2ME;


import interfaces.IParaMedView;

import javax.microedition.midlet.*;
import javax.microedition.lcdui.*;

import proxy.ParaMedControllerProxy2ME;


public class ParaMed2MEGUI extends MIDlet implements IParaMedView
{
	private ParaMedControllerProxy2ME paraMedController;
	
	private Display display;
	private Command exitCommand;
	private Form mainForm;
	public ParaMed2MEGUI()
	{
		 paraMedController = new ParaMedControllerProxy2ME(this);
		
		 display = Display.getDisplay(this);
		 mainForm = new Form("ParaMed2ME");
		 

		 exitCommand = new Command("Exit", Command.SCREEN, 1);
		 mainForm.addCommand(exitCommand);
		 
		 mainForm.setCommandListener(paraMedController); 
	}
	public void startApp()
	{
		  display.setCurrent(mainForm);
	}

	public void pauseApp()
	{
	}
	
	public void destroyApp(boolean incondicional)
	{
	}
	
	public void terminateApplication() {
		destroyApp(false);

		notifyDestroyed();
	}

}