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

public class SkeletonPicker extends JComponent{
	public SkeletonPicker() {
		setLayout(new SpringLayout());
        ((SpringLayout)getLayout()).getConstraints(this).setWidth(Spring.constant(510));
        ((SpringLayout)getLayout()).getConstraints(this).setHeight(Spring.constant(680));

		// jcomponent related
		setBorder(BorderFactory.createLineBorder(Color.BLUE));
        setVisible(true);
	}
	public void addHotSpot(int x, int y) {
		HotSpot aux = new HotSpot((SpringLayout)getLayout(), x, y);
		add(aux);
	}
	
	public void addBackground() {
      JLabel image = new JLabel(new ImageIcon("resources\\skeleton.gif"));
      ((SpringLayout)getLayout()).getConstraints(image).setWidth(Spring.constant(510));
      ((SpringLayout)getLayout()).getConstraints(image).setHeight(Spring.constant(680));
      add(image);
	}
	
}
