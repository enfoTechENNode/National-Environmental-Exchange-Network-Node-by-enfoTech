package Node.API;

import DataFlow.Component.Interface.*;
/**
 * <p>Implement ActionPrarameter</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * 
 * @author enfoTech
 * @version 2.0
 */

public class ActionParameter implements IActionParameter {
    private String name;
    private Object value;
    private String type;
    private int direction = ActionParameterDirection.Input;
    
    /**
     * Gets the parameter direction value. The values of direction are defined in ActionParameterDirection class. 
     */
	public int getDirection() {
		return direction;
	}
	
	/**
	 * Sets the parameter direction. The values of direction are defined in ActionParameterDirection class.
	 */
	public void setDirection(int direction) {
		this.direction = direction;
	}
	
	/**
	 * Gets the parameter name.
	 */
	public String getParameterName() {
		return name;
	}
	
	/**
	 * Sets the parameter name.
	 */
	public void setParameterName(String name) {
		this.name = name;
	}
	
	/**
	 * Gets the parameter type.
	 */
	public String getParameterType() {
		return type;
	}
	
	/**
	 * Sets the parameter type.
	 */
	public void setParameterType(String type) {
		this.type = type;
	}
	
	/**
	 * Gets the parameter value.
	 */
	public Object getParameterValue() {
		return value;
	}
	
	/**
	 * Sets the parameter value.
	 */
	public void setParameterValue(Object value) {
		this.value = value;
	}
    
    
}
