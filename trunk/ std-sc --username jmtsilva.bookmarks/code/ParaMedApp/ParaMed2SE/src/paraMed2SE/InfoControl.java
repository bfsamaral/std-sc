package paraMed2SE;

import java.awt.geom.Point2D;
import java.util.ArrayList;

import org.w3c.dom.NamedNodeMap;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;

public class InfoControl {

	private InfoControlType type;
	private int id;
	private int size;
	private int x, y;
	private String description;
	private ArrayList<String> listComboData = new ArrayList<String>();
	
	public InfoControl(Node controlNode) 
	{
		NamedNodeMap atribs = controlNode.getAttributes();
		if(  atribs.getNamedItem("type").getNodeValue().equals("edit") )
				type = InfoControlType.EDIT;
		if( atribs.getNamedItem("type").getNodeValue().equals("static") )
				type = InfoControlType.STATIC;
		if( atribs.getNamedItem("type").getNodeValue().equals("combo") )
				type = InfoControlType.COMBO;
		if( atribs.getNamedItem("type").getNodeValue().equals("check") )
			type = InfoControlType.CHECK;
		if( atribs.getNamedItem("type").getNodeValue().equals("text") )
			type = InfoControlType.TEXT;

		id          = Integer.parseInt(atribs.getNamedItem("id").getNodeValue());
		size        = Integer.parseInt(atribs.getNamedItem("sz").getNodeValue());
		x           = Integer.parseInt(atribs.getNamedItem("x").getNodeValue());
		y           = Integer.parseInt(atribs.getNamedItem("y").getNodeValue());
		description = atribs.getNamedItem("value").getNodeValue();
		
		// combo é especifico
		if(type == InfoControlType.COMBO) 
		{
			NodeList items = controlNode.getChildNodes();
			for(int j=0;j<items.getLength(); ++j) 
			{
				Node n = items.item(j);
				if(n.getNodeType() == Node.TEXT_NODE) continue;
				listComboData.add(n.getTextContent());
			}
		}
	}

	public String getDescription() {
		return description;
	}

	public int getId() {
		return id;
	}

	public ArrayList<String> getListComboData() {
		return listComboData;
	}

	public int getSize() {
		return size;
	}

	public InfoControlType getType() {
		return type;
	}

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}
	
	
	
}
