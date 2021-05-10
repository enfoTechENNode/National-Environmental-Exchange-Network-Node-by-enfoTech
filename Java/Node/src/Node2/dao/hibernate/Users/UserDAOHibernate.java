package Node2.dao.hibernate.Users;

import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;
import Node2.dao.Users.UserDAO;
import Node2.model.Users.User;


/**
 * <p>This class interacts with Spring and Hibernate to save and
 * retrieve User objects.</p>
 *
 * @author enfotech
 */
public class UserDAOHibernate extends HibernateDaoSupport implements UserDAO {
    private static Log log = LogFactory.getLog(UserDAOHibernate.class);

	  /**
	   * getUsers
	   * @param 
	   * @return List
	   */
    public List getUsers() {
        return getHibernateTemplate().find("from User");
    }

	  /**
	   * getUser
	   * @param id
	   * @return User
	   */
    public User getUser(Long id) {
        return (User) getHibernateTemplate().get(User.class, id);
    }

	  /**
	   * saveUser
	   * @param user
	   * @return 
	   */
    public void saveUser(User user) {
        getHibernateTemplate().saveOrUpdate(user);

        if (log.isDebugEnabled()) {
            log.debug("userId set to: " + user.getId());
        }
    }

	  /**
	   * removeUser
	   * @param id
	   * @return 
	   */
    public void removeUser(Long id) {
        Object user = getHibernateTemplate().load(User.class, id);
        getHibernateTemplate().delete(user);
    }
}
