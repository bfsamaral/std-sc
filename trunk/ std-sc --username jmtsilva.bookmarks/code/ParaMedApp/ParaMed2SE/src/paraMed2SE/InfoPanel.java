package paraMed2SE;

import java.awt.Color;
import java.awt.Dimension;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Iterator;

import javax.swing.ImageIcon;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;


public class InfoPanel extends JPanel implements HotSpotListener {
	
	private DataPanel dataPanel                = new DataPanel(); 
	private SkeletonPickerPanel skeletonPicker = new SkeletonPickerPanel(); 

	private Dimension panelSize;
	static Document document;

	private ArrayList<HotSpotInfoNode> hotSpotInfos = new ArrayList<HotSpotInfoNode>();   
	
	private Hashtable tabsId = new Hashtable();
	
	private int currentTabId = 0;
	
	private int numOfHotSpots;
	
	public InfoPanel(Dimension size) {
		panelSize  = size;
		buildAndAddTabEditor();
	    buildAndAddSkeletonPicker();
	}
	
	private void buildAndAddTabEditor() {
		dataPanel.setPreferredSize( new Dimension( (int)panelSize.width , (int)(panelSize.height)) );
		add(dataPanel);
	}
	
	private void buildAndAddSkeletonPicker() {
		skeletonPicker.addHotSpotListener(this);
		add(skeletonPicker);
	}
	
	public void loadXML(String xmlConfigFilename)
	{
		try {
			DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
			DocumentBuilder builder = factory.newDocumentBuilder();
				document = builder.parse( new File(xmlConfigFilename) );
			} catch (SAXException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (ParserConfigurationException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			
			int garbage = 0;
			NodeList list = document.getFirstChild().getChildNodes();
			int i;
			for(i=0; i<list.getLength(); ++i) 
			{
				if( list.item(i).getNodeType() == Node.TEXT_NODE ) {
					++garbage;
					continue;
				}
				HotSpotInfoNode hsi = new HotSpotInfoNode(list.item(i));
				// the first tab is personal info
				if( (i-garbage) == 0 )
				{
					dataPanel.addHotSpotTab(hsi);
				}
				
				skeletonPicker.addHotSpot(hsi, i-garbage); 
				tabsId.put(new Integer(i-garbage), -1);	// -1 because there is no association to a tab
				hotSpotInfos.add(hsi);
			}
			skeletonPicker.addBackground();
			numOfHotSpots = i - garbage;
		}
	
		public void hotSpotClicked(int id, HotSpot hsClicked) {
			if( ((Integer)tabsId.get(new Integer(id))).intValue() == -1) 
			{
				// not added, so add the new tab
				HotSpotInfoNode hs = hotSpotInfos.get(id);
				dataPanel.addHotSpotTab(hs);
				tabsId.put(id, ++currentTabId);
				
				hsClicked.toggleState();
			}
			else 
			{
				// just remove the tab
				
				HotSpotInfoNode hs = hotSpotInfos.get(id);
				String question = "Del Tab " + hs.getName()  + "? That Data will be lost!";
				if ( JOptionPane.showConfirmDialog(this, question, "Delete Confirmation", JOptionPane.YES_NO_OPTION, JOptionPane.QUESTION_MESSAGE, null) == JOptionPane.NO_OPTION) 
					return;
				
				--currentTabId;
				
				hsClicked.toggleState();

				int tabPos = ((Integer)tabsId.get(new Integer(id))).intValue();
				dataPanel.removeTabAt( tabPos );
				tabsId.put(new Integer(id), -1);
				
				for(int i=0; i<numOfHotSpots; ++i) 
				{
					int tabPosAux = ((Integer)tabsId.get(new Integer(i))).intValue();
					if( tabPosAux>tabPos )
						tabsId.put(new Integer(i), --tabPosAux);
				}
				
				
//				for (Iterator it = tabsId.values().iterator(); it.hasNext();) {
//					Integer value = (Integer) it.next();
//					if (value.intValue() > tabPos) 
//					{
//						int newValue = value.intValue() - 1;
//												
//						//value = new Integer(newValue);
//					}
//				}

				
			}
			
			//dataPanel.setTabComponentAt(id,);
			//dataPanel.setFocusTab(id);
		}
	}
