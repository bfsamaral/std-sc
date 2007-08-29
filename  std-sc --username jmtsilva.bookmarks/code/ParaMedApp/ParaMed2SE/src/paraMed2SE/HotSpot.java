package paraMed2SE;

import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

import javax.swing.ImageIcon;
import javax.swing.JLabel;
import javax.swing.Spring;
import javax.swing.SpringLayout;

public class HotSpot extends JLabel implements HotSpotNotifier {
	private enum State {ENABLED, DISABLED};
	 
	private int id;
	
	private HotSpotListener listener;
	
	private HotSpot me;
	
	private final String enabledImg = "resources\\hotSpot_red.gif";
	private final String disabledImg = "resources\\hotSpot_green.gif";
	private State state = State.DISABLED;
	
	public HotSpot(SpringLayout currLayout, int x, int y, int hsid) {
		id = hsid;
		me = this;
		setLayout(new SpringLayout());
		currLayout.getConstraints(this).setWidth(Spring.constant(30));
		currLayout.getConstraints(this).setHeight(Spring.constant(30));
		currLayout.getConstraints(this).setX(Spring.constant(x));
		currLayout.getConstraints(this).setY(Spring.constant(y));
		setIcon(new ImageIcon(disabledImg));
		
		this.addMouseListener(new MouseListener() 
		{

			public void mouseClicked(MouseEvent arg0) {
				if (arg0.getClickCount() == 2 )
				{
					//toggleState();
					listener.hotSpotClicked(id, me);
				}
				else
				{
					//listener.hotSpotClicked(id);
				}
			}

			public void mouseEntered(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			public void mouseExited(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			public void mousePressed(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}

			public void mouseReleased(MouseEvent arg0) {
				// TODO Auto-generated method stub
				
			}
		});
	}
	
	public void toggleState() {
		if(state == State.DISABLED) {
			state = State.ENABLED;
			setIcon(new ImageIcon(enabledImg));
		}
		else {
			state = State.DISABLED;
			setIcon(new ImageIcon(disabledImg));
		}
	}

	public void addHotSpotListener(HotSpotListener hslistener) 
	{
		listener = hslistener;
	}
}
