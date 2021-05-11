package Node.DB.Interfaces.Configuration;

import EnfoTech.Task.XjavaClass;
import EnfoTech.Task.Xschedule;
import EnfoTech.Task.Xservice;
import EnfoTech.Task.Xtask;

import Node.Biz.Administration.Schedule;
/**
 * <p>This class create ITaskConfiguration interface.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface ITaskConfiguration {
  /**
   * Get List of Tasks
   * @return Xtask[] Array of Tasks, null if not found
   */
  public Xtask[] GetTasks ();

  /**
   * Get Service
   * @param taskName String Service Name
   * @return Xservice Service, null if not found
   */
  public Xservice GetService (String serviceName);

  /**
   * Get Java Class
   * @param name String Name of Java Class
   * @return XjavaClass Java Class, null if not found
   */
  public XjavaClass GetJavaClass (String name);

  /**
   * Get Schedule
   * @param name String Name of Schedule
   * @return Xschedule Schedule, null if not found
   */
  public Xschedule GetSchedule (String name);

  /**
   * Get Time Last Updated
   * @return long Time
   */
  public long GetUpdatedDTTM ();

  // Email Template
	/**
	 * GetTaskEmailSender.
	 * @param .
	 * @return String
	 */
  public String GetTaskEmailSender ();
	/**
	 * GetTaskEmailSenderEmail.
	 * @param .
	 * @return String
	 */
  public String GetTaskEmailSenderEmail ();
	/**
	 * GetTaskEmailUID.
	 * @param .
	 * @return String
	 */
  public String GetTaskEmailUID ();
	/**
	 * GetTaskEmailPWD.
	 * @param .
	 * @return String
	 */
  public String GetTaskEmailPWD ();
	/**
	 * GetTaskEmailCCList.
	 * @param .
	 * @return String[]
	 */
  public String[] GetTaskEmailCCList ();
	/**
	 * GetTaskEmailBCCList.
	 * @param .
	 * @return String[]
	 */
  public String[] GetTaskEmailBCCList ();
	/**
	 * GetEmailTempate.
	 * @param .
	 * @return String
	 */
  public String GetEmailTempate();

  // Save CC Lists
	/**
	 * SaveTaskCCList.
	 * @param ccList.
	 * @return boolean
	 */
  public boolean SaveTaskCCList (String[] ccList);
	/**
	 * SaveTaskBCCList.
	 * @param bccList.
	 * @return boolean
	 */
  public boolean SaveTaskBCCList (String[] bccList);

// save task status
	/**
	 * SaveTaskStatus.
	 * @param myXTask.
	 * @return boolean
	 */
  public boolean SaveTaskStatus (Xtask myXTask);

  /**
   * Save Task without updating date
   * @param taskName String
   * @param className String
   * @param methodName String
   * @param paramNames String[]
   * @param values String[]
   * @param schedule Schedule
   * @return boolean true if successful, false otherwise
   */
  public boolean SaveTaskWithoutUpdatingDate (String taskName, String className, String methodName, String[] paramNames, String[] paramTypes, String[] values, Schedule schedule);

  /**
   * Save Task
   * @param taskName String
   * @param className String
   * @param methodName String
   * @param paramNames String[]
   * @param values String[]
   * @param schedule Schedule
   * @return boolean true if successful, false otherwise
   */
  public boolean SaveTask (String taskName, String className, String methodName, String[] paramNames, String[] paramTypes, String[] values, Schedule schedule);


  /**
   * Delete Task
   * @param taskName String
   * @return boolean true if successful, false otherwise
   */
  public boolean DeleteTask (String taskName);
}
