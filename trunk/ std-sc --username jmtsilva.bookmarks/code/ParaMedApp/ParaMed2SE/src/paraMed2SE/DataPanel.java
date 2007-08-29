package paraMed2SE;

import java.awt.Component;
import java.awt.Dimension;
import java.awt.Panel;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JComponent;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTabbedPane;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.Spring;
import javax.swing.SpringLayout;
import javax.xml.crypto.Data;
import javax.xml.parsers.DocumentBuilder; 
import javax.xml.parsers.DocumentBuilderFactory;  
import javax.xml.parsers.FactoryConfigurationError;  
import javax.xml.parsers.ParserConfigurationException;


import org.xml.sax.SAXException;  
import org.xml.sax.SAXParseException;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.Constructor;
import java.util.ArrayList;


import org.w3c.dom.Document;
import org.w3c.dom.DOMException;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;


public class DataPanel extends JTabbedPane {
		
	public DataPanel() 
	{
		setTabLayoutPolicy(JTabbedPane.SCROLL_TAB_LAYOUT);
	}
	
	public void addHotSpotTab(HotSpotInfoNode node) 
	{
		ArrayList<InfoControl> infoControls = node.getInfoControls();
		JPanel panel = new JPanel();
		panel.setLayout(new SpringLayout());
		
		for(int i=0; i<infoControls.size(); ++i) 
		{
			InfoControl f = infoControls.get(i);

			// static
			if(f.getType() == InfoControlType.STATIC)
			{
					JLabel label = new JLabel();
					label.setText(f.getDescription());
					label.setLayout(new SpringLayout());
					((SpringLayout)panel.getLayout()).getConstraints(label).setX(Spring.constant(f.getX()));
					((SpringLayout)panel.getLayout()).getConstraints(label).setY(Spring.constant(f.getY()));
					panel.add(label);
					continue;
			}
			
			// text
			if(f.getType() == InfoControlType.EDIT)
			{
					JTextField text = new JTextField(f.getDescription(), f.getSize());
					text.setText(f.getDescription());
					text.setLayout(new SpringLayout());
					((SpringLayout)panel.getLayout()).getConstraints(text).setX(Spring.constant(f.getX()));
					((SpringLayout)panel.getLayout()).getConstraints(text).setY(Spring.constant(f.getY()));
					panel.add(text);
					continue;
			}
			
			// combo
			if(f.getType() == InfoControlType.COMBO)
			{
					JComboBox text = new JComboBox();
					ArrayList<String> cbData = f.getListComboData();
					for(int j=0; j<cbData.size(); ++j) 
					{
						text.addItem("         " + cbData.get(j) + "   ");
					}					
					text.setLayout(new SpringLayout());
					((SpringLayout)panel.getLayout()).getConstraints(text).setX(Spring.constant(f.getX()));
					((SpringLayout)panel.getLayout()).getConstraints(text).setY(Spring.constant(f.getY()));
					panel.add(text);
					continue;
			}
			
			//check
			if(f.getType() == InfoControlType.CHECK)
			{
					JCheckBox text = new JCheckBox();
					text.setText(f.getDescription());
					text.setLayout(new SpringLayout());
					((SpringLayout)panel.getLayout()).getConstraints(text).setX(Spring.constant(f.getX()));
					((SpringLayout)panel.getLayout()).getConstraints(text).setY(Spring.constant(f.getY()));
					panel.add(text);
					continue;
			}
			
			//textArea
			if(f.getType() == InfoControlType.TEXT)
			{
					JTextArea text = new JTextArea(f.getSize()/4, f.getSize()/2);
					text.setText(f.getDescription());
					text.setLayout(new SpringLayout());
					((SpringLayout)panel.getLayout()).getConstraints(text).setX(Spring.constant(f.getX()));
					((SpringLayout)panel.getLayout()).getConstraints(text).setY(Spring.constant(f.getY()));
					panel.add(text);
					continue;
			}
		}
		addTab(node.getName(), new ImageIcon("resources\\symptoms\\" + node.getImg()), panel);
		/*
		if( getTabCount()==0 )
			
		*/
		
		//int tabCount = getTabCount();
		/*
		if(tabCount != 1)
			setEnabledAt(tabCount-1, false);
		*/
	}
	
	public void setFocusTab(int id) 
	{
		setSelectedIndex(id);
	}
}

