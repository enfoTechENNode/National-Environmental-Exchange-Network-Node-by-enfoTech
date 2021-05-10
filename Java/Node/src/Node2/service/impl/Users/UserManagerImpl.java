package Node2.service.impl.Users;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import Node2.dao.Users.UserDAO;
import Node2.model.Users.User;
import Node2.service.Users.UserManager;

/**
 * <p>This class create UserManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class UserManagerImpl implements UserManager {
    private static Log log = LogFactory.getLog(UserManagerImpl.class);
    private UserDAO dao;

	/**
     * setUserDAO
     * @param dao
     * @return 
     */
    public void setUserDAO(UserDAO dao) {
        this.dao = dao;
    }

	/**
     * getUsers
     * @param 
     * @return List
     */
    public List getUsers() {
        return dao.getUsers();
    }

	/**
     * getUser
     * @param userId
     * @return User
     */
    public User getUser(String userId) {
        User user = dao.getUser(Long.valueOf(userId));

        if (user == null) {
            log.warn("UserId '" + userId + "' not found in database.");
        }

        return user;
    }

	/**
     * saveUser
     * @param user
     * @return User
     */
    public User saveUser(User user) {
        dao.saveUser(user);

        return user;
    }

	/**
     * removeUser
     * @param userId
     * @return 
     */
    public void removeUser(String userId) {
        dao.removeUser(Long.valueOf(userId));
    }
}
