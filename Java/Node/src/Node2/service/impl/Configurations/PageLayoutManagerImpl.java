package Node2.service.impl.Configurations;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import Node2.dao.Configurations.PageLayoutDAO;
import Node2.model.Configurations.PageLayout;
import Node2.service.Configurations.PageLayoutManager;
import Node2.service.impl.Users.UserManagerImpl;

/**
 * <p>This class create PageLayoutManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class PageLayoutManagerImpl implements PageLayoutManager {
    private static Log log = LogFactory.getLog(UserManagerImpl.class);

    private PageLayoutDAO dao;

	/**
     * setPageLayoutDAO
     * @param dao
     * @return 
     */
    public void setPageLayoutDAO(PageLayoutDAO dao) {
        this.dao = dao;
    }

    
	/**
     * getPageLayout
     * @param userId
     * @return PageLayout
     */
    public PageLayout getPageLayout(String userId){
    	    	
		return dao.getPageLayout(new Long(userId));
    	
    }
    
	/**
     * setPageLayout
     * @param pageLayout
     * @param insertOrupdate
     * @return boolean
     */
	public boolean setPageLayout(PageLayout pageLayout,String insertOrupdate){
		 return dao.setPageLayout(pageLayout,insertOrupdate);
	}
	
	/**
     * deletePageLayout
     * @param userId
     * @return boolean
     */
	public boolean deletePageLayout(Long userId){
		 return dao.deletePageLayout(userId);		
	};	

}
