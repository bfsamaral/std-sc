package paraMed2SE;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.net.URL;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JTabbedPane;
import javax.swing.JToolBar;
import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;

import org.jvnet.substance.SubstanceLookAndFeel;
import org.jvnet.substance.skin.SubstanceAutumnLookAndFeel;
import org.jvnet.substance.theme.SubstanceAquaTheme;
import org.jvnet.substance.theme.SubstanceBarbyPinkTheme;

import contrib.com.blogofbug.swing.components.ImageLabel;

import proxy.ParaMedControllerProxy2SE;

import interfaces.IParaMedView;


public class ParaMed2SEGUI extends JFrame implements IParaMedView{

	private Dimension screenSize;

	private ParaMedControllerProxy2SE paraMedController;
	
	private InfoPanel infoPanel;
	private JMenuBar menubar = new JMenuBar();
	private JToolBar toolbar = new JToolBar();

	public ParaMed2SEGUI() {
		paraMedController = new ParaMedControllerProxy2SE(this);

		// set look and feel	
		try {
			UIManager.setLookAndFeel( new SubstanceLookAndFeel());
			SubstanceLookAndFeel.setCurrentTheme(new SubstanceAquaTheme());
		} catch (UnsupportedLookAndFeelException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		setLayout(new BorderLayout());
		
		screenSize = Toolkit.getDefaultToolkit().getScreenSize();
	
        buildAndAddMenu();
		buildAndAddToolBar();
		buildAndAddInfoPanel();
		// misc last things
		
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setTitle("ParaMed2SE");
        pack();
        setVisible(true);
        setExtendedState(MAXIMIZED_BOTH);
        setResizable(true);
        setAlwaysOnTop(true);
	}
	
	private void buildAndAddInfoPanel() 
	{
		infoPanel = new InfoPanel(new Dimension( (int)(screenSize.width * 0.58) , (int)(screenSize.height * 0.85)));
		infoPanel.loadXML("D:\\Bruno\\ISEL\\pfc\\code\\ParaMedApp\\xml4config.xml");
		//infoPanel.setPreferredSize();
		getContentPane().add(infoPanel, BorderLayout.CENTER);
	}
	
	private void buildAndAddToolBar() {
		// adding buttons to the toolbar
		toolbar.add(makeNavigationButton("New24.gif", "", "New", ""));
		toolbar.add(makeNavigationButton("Open24.gif", "", "Open", ""));
		toolbar.add(makeNavigationButton("Save24.gif", "", "Save", ""));
		toolbar.add(makeNavigationButton("Close24.gif", "", "Close", ""));
		toolbar.add(makeNavigationButton("Find24.gif", "", "Find", ""));
		toolbar.add(makeNavigationButton("Help24.gif", "", "Help", ""));		
		toolbar.setFloatable(false);
		toolbar.setRollover(true);
		getContentPane().add(toolbar, BorderLayout.PAGE_START);
	}

	private void buildAndAddMenu() {
		JMenuItem auxMenuItem;
		JMenu auxMenu;
		
		// File menu
		auxMenu = new JMenu("File");
		auxMenuItem = new JMenuItem("New", new ImageIcon("resources\\file_new.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		auxMenu.addSeparator();
		auxMenuItem = new JMenuItem("Open", new ImageIcon("resources\\file_open.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		auxMenuItem = new JMenuItem("Save", new ImageIcon("resources\\file_save.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		auxMenu.addSeparator();
		auxMenuItem = new JMenuItem("Exit", new ImageIcon("resources\\file_exit.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		menubar.add(auxMenu);
		
		// Find Menu
		auxMenu = new JMenu("Find");
		auxMenuItem = new JMenuItem("Patient", new ImageIcon("resources\\find_patient.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		menubar.add(auxMenu);
		

		// Sync Menu
		auxMenu = new JMenu("Sync");
		auxMenuItem = new JMenuItem("Current", new ImageIcon("resources\\sync_current.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		auxMenuItem = new JMenuItem("All", new ImageIcon("resources\\sync_all.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		menubar.add(auxMenu);
		
		//	Send Menu
		auxMenu = new JMenu("Send");
		auxMenuItem = new JMenuItem("Current", new ImageIcon("resources\\send_current.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		auxMenuItem = new JMenuItem("All", new ImageIcon("resources\\send_all.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		menubar.add(auxMenu);
		
		//	Sync Menu
		auxMenu = new JMenu("Preferences");
		auxMenuItem = new JMenuItem("Environment", new ImageIcon("resources\\env_current.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		auxMenuItem = new JMenuItem("All", new ImageIcon("resources\\env_all.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		menubar.add(auxMenu);
		
		//	Help Menu
		auxMenu = new JMenu("Help");
		auxMenuItem = new JMenuItem("Help", new ImageIcon("resources\\help_help.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		auxMenuItem = new JMenuItem("About", new ImageIcon("resources\\help_about.gif"));
		auxMenuItem.addActionListener(paraMedController);
		auxMenu.add(auxMenuItem);
		menubar.add(auxMenu);
		
		setJMenuBar(menubar);
	}
	
	protected JButton makeNavigationButton(
			String imageName, String actionCommand,
            String toolTipText, String altText) 
	{
			//Look for the image.
			String imgLocation = "resources\\toolbar\\" + imageName;
			//URL imageURL = class.getResource(imgLocation);
			
			//Create and initialize the button.
			JButton button = new JButton();
			button.setActionCommand(actionCommand);
			button.setToolTipText(toolTipText);
			//button.addActionListener(this);
			//button.setText(altText);
			button.setIcon(new ImageIcon(imgLocation, altText));
			return button;
	}
	
	public void terminateApplication() {
		this.dispose();
	}
}
