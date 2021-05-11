package Node2.web;

import java.io.IOException;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;

import Node.Phrase;

import com.enfotech.basecomponent.jndi.JNDIAccess;
/**
 * <p>This class create AuthenticateFilter.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class AuthenticateFilter extends HttpServlet implements Filter {
	protected FilterConfig config;
	private String redirectURl = null; 
	  /**
	   * init
	   * @param filterConfig
	   * @return 
	   */
	public void init(FilterConfig filterConfig) throws ServletException {
		this.config = filterConfig;
		redirectURl = "/Page/Entry/Login.do?invalid=true";   
	}

	  /**
	   * doFilter
	   * @param request
	   * @param response
	   * @param chain
	   * @return 
	   */
	public void doFilter(ServletRequest request, ServletResponse response,
			FilterChain chain) throws IOException, ServletException {
		RequestDispatcher dispatcher = request.getRequestDispatcher(redirectURl);
		HttpServletRequest userRequest = (HttpServletRequest) request;
		HttpSession session = userRequest.getSession();
	    String uri = userRequest.getRequestURI().toString();
	    int sessionTimeOut = (new Integer((String) JNDIAccess.GetJNDIValue(Phrase.STATUS_SESSION_TIMEOUT, false))).intValue()/3; // since the front will send two times ajax request every checking time.
	    if (uri != null && !uri.equalsIgnoreCase("/Node.Administration/Page/Entry/Login.do")) {
			if (session.getAttribute(Phrase.USER_SESSION) != null && !session.getAttribute(Phrase.USER_SESSION).equals("") && session.getAttribute(Phrase.STATUS_SESSION_COUNTER) != null) {   
				int statusIntervalCounter = ((Integer)session.getAttribute(Phrase.STATUS_SESSION_COUNTER)).intValue();
				if(uri.equalsIgnoreCase("/Node.Administration/Page/Entry/status.do"))
					statusIntervalCounter++;
				else
					statusIntervalCounter = 0;					
				//System.out.println("statusIntervalCounter is: "+statusIntervalCounter + "   sessionTimeOut is: "+sessionTimeOut + "  The system is: " + Utility.GetSysTimeString());
				if(statusIntervalCounter==sessionTimeOut){
					if (session.getAttribute(Phrase.USER_SESSION) != null){
						session.removeAttribute(Phrase.USER_SESSION);
					}
					if (session.getAttribute(Phrase.STATUS_SESSION_COUNTER) != null){
						session.removeAttribute(Phrase.STATUS_SESSION_COUNTER);
					}
					session.invalidate();
					if(uri.equalsIgnoreCase("/Node.Administration/Page/Entry/Dashboard.jsp")){
						dispatcher = request.getRequestDispatcher("/Page/Entry/Login.do?sessionTimeOut=true");
					}
					dispatcher.forward(request,response); 
				}else{
					session.setAttribute(Phrase.STATUS_SESSION_COUNTER, new Integer(statusIntervalCounter));
					chain.doFilter(request, response);					
				}
			}else if(uri.equalsIgnoreCase("/Node.Administration/Page/Entry/Dashboard.jsp")){
				dispatcher = request.getRequestDispatcher("/Page/Entry/Login.do?sessionTimeOut=true");
				dispatcher.forward(request,response);
			}else dispatcher.forward(request,response); 	    	
	    }else chain.doFilter(request, response);
	}

	  /**
	   * destroy
	   * @param 
	   * @return 
	   */
	public void destroy() {
		config = null;
	}

}
