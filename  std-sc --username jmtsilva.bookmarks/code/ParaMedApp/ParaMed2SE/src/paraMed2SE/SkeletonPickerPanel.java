package paraMed2SE;

import java.awt.Color;
import java.awt.FlowLayout;
import java.awt.LayoutManager;
import java.awt.Rectangle;
import java.util.ArrayList;

import javax.swing.BorderFactory;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComponent;
import javax.swing.JLabel;
import javax.swing.Spring;
import javax.swing.SpringLayout;
import javax.swing.border.Border;

import org.w3c.dom.Node;

public class SkeletonPickerPanel extends JComponent implements HotSpotListener, HotSpotNotifier{
	
	private HotSpotListener hotSpotListener;
	
	public SkeletonPickerPanel() {
		setLayout(new SpringLayout());
        ((SpringLayout)getLayout()).getConstraints(this).setWidth(Spring.constant(510));
        ((SpringLayout)getLayout()).getConstraints(this).setHeight(Spring.constant(680));

		// jcomponent related
		setBorder(BorderFactory.createLineBorder(Color.BLUE));
        setVisible(true);
	}
	
	private void addHotSpot(int x, int y, int id) {
		if(x<0) return;
		HotSpot aux = new HotSpot((SpringLayout)getLayout(), x, y, id);
		aux.addHotSpotListener(this);
		add(aux);
	}
	
	public void addBackground() {
      JLabel image = new JLabel(new ImageIcon("resources\\skeleton.gif"));
      ((SpringLayout)getLayout()).getConstraints(image).setWidth(Spring.constant(510));
      ((SpringLayout)getLayout()).getConstraints(image).setHeight(Spring.constant(680));
      add(image);
	}
	
	public void addHotSpot(HotSpotInfoNode node, int id) 
	{
		addHotSpot(node.getX(), node.getY(), id);
	}

	public void hotSpotClicked(int id, HotSpot hs) {
		hotSpotListener.hotSpotClicked(id, hs);
	}

	public void addHotSpotListener(HotSpotListener hslistener) {
		hotSpotListener = hslistener;
	}
	
	
	/*
	public void addListener(HotSpotListener listener) 
	{
		
	}
	*/
	
}
