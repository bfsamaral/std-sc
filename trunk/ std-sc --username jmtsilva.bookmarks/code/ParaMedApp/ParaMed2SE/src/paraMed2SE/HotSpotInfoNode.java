package paraMed2SE;

import java.awt.geom.Point2D;
import java.util.ArrayList;

import org.w3c.dom.NamedNodeMap;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

public class HotSpotInfoNode {

	private final static String hotSpotImgLocation = ".\\resources\\hotspot\\";
	
	private String name;
	private String img;
	private int x, y;
	private ArrayList<InfoControl> infoControls = new ArrayList<InfoControl>();
	
	public HotSpotInfoNode(Node domNode) 
	{
		NamedNodeMap atribs = domNode.getAttributes();
		name = atribs.getNamedItem("name").getNodeValue();
		img = atribs.getNamedItem("img").getNodeValue();
		x = Integer.parseInt(domNode.getFirstChild().getNextSibling().getAttributes().getNamedItem("x").getNodeValue());
		y = Integer.parseInt(domNode.getFirstChild().getNextSibling().getAttributes().getNamedItem("x").getNodeValue());
		
		NodeList list = domNode.getFirstChild().getNextSibling().getNextSibling().getNextSibling().getChildNodes();
		
		for(int i=0; i<list.getLength(); ++i) 
		{
			if( list.item(i).getNodeType() == Node.TEXT_NODE ) continue;
			InfoControl infoControl = new InfoControl(list.item(i));
			infoControls.add(infoControl);			
		}
	}

	// serialize to specific format
	public void load(String fileName) 
	{
		
	}
	
	// serialize to specific format	
	public void save(String fileName)  
	{
		
	}

	public static String getHotSpotImgLocation() {
		return hotSpotImgLocation;
	}

	public String getImg() {
		return img;
	}

	public ArrayList<InfoControl> getInfoControls() {
		return infoControls;
	}

	public String getName() {
		return name;
	}

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}
	
	
	
}
