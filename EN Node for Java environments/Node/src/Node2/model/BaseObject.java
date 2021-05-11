package Node2.model;

import java.io.Serializable;

import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.ToStringStyle;


/**
 * <p>Base class for Model objects.  This is basically for the toString, equals
 * and hashCode methods.</p>
 *
 * @author enfoTech
 */
public class BaseObject implements Serializable {
	/**
     * toString
     * @param 
     * @return String
     */
    public String toString() {
        return ToStringBuilder.reflectionToString(this,
                ToStringStyle.MULTI_LINE_STYLE);
    }

	/**
     * equals
     * @param 
     * @return boolean
     */
    public boolean equals(Object o) {
        return EqualsBuilder.reflectionEquals(this, o);
    }

	/**
     * hashCode
     * @param 
     * @return int
     */
    public int hashCode() {
        return HashCodeBuilder.reflectionHashCode(this);
    }
}
