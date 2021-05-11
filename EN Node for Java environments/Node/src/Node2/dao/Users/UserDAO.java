package Node2.dao.Users;

import java.util.List;

import Node2.dao.DAO;
import Node2.model.Users.User;

/**
 * <p>This class create UserDAO.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface UserDAO extends DAO {
	  /**
	   * getUsers
	   * @param 
	   * @return List
	   */
    public List getUsers();

	  /**
	   * getUser
	   * @param userId
	   * @return User
	   */
    public User getUser(Long userId);

	  /**
	   * saveUser
	   * @param user
	   * @return 
	   */
    public void saveUser(User user);

	  /**
	   * removeUser
	   * @param userId
	   * @return 
	   */
    public void removeUser(Long userId);
}
