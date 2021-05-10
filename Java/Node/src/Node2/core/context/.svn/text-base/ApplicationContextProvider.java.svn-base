package Node2.core.context;

import org.springframework.beans.BeansException;
import org.springframework.context.ApplicationContext;
import org.springframework.context.ApplicationContextAware;


/* <p>This class provides an application-wide access to the  
 * Spring ApplicationContext! The ApplicationContext is  1
 * injected in a static method of the class "AppContext". 
 *  
 * Use AppContext.getApplicationContext() to get access  
 * to all Spring Beans.  </p>
 *  
 * @author enfotech  
 */  

public class ApplicationContextProvider implements ApplicationContextAware {

    private static ApplicationContext applicationContext = null;

    /**
     * getApplicationContext
     * @param 
     * @return ApplicationContext
     */
    public static ApplicationContext getApplicationContext() {
        return applicationContext;
    }
    /**
     * setApplicationContext
     * @param applicationContext
     * @return 
     */
    public void setApplicationContext(ApplicationContext applicationContext) throws BeansException {
          // Assign the ApplicationContext into a static variable
         this.applicationContext = applicationContext;
    }

}
