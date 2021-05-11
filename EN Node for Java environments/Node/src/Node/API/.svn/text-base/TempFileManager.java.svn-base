package Node.API;

import java.io.*;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.ArrayList;
import Node.Utils.AppUtils;

/**
 * <p>Manage temporary file</p>
 * <p>Company: enfoTech & Consulting, Inc.
 * </p>
 * 
 * @author enfoTech
 * @version 2.0
 */

public class TempFileManager
{

  /**
   *  Creates a temporary file in the proper directory to allow for cleanup
   *  after execution. This method delegates to {@link
   *  File#createTempFile(java.lang.String, java.lang.String, java.io.File)} so
   *  refer to it for more documentation. Any file created using this method
   *  should be considered as deleted at JVM exit; therefore, do not use this
   *  method to create files that need to be persistent between application
   *  runs.
   *
   * @param  prefix        the prefix string used in generating the file name;
   *      must be at least three characters long
   * @param  suffix        the suffix string to be used in generating the file's
   *      name; may be null, in which case the suffix ".tmp" will be used
   * @return               an abstract pathname denoting a newly created empty
   *      file
   * @throws  IOException  if a file could not be created
   */
  public static File createTempFile(String prefix, String suffix)
    throws IOException, Exception
  {
    // Check to see if you have already initialized a temp directory
    // for this class.
    if (sTmpDir == null)
    {
      // Initialize your temp directory. You use the java temp directory
      // property, so you are sure to find the files on the next run.
      String tmpDirName = System.getProperty("java.io.tmpdir");
      File tmpDir = File.createTempFile(TEMP_DIR_PREFIX, ".tmp",
          new File(tmpDirName));

      // Delete the file if one was automatically created by the JVM.
      // You are going to use the name of the file as a directory name,
      // so you do not want the file laying around.
      tmpDir.delete();

      // Create a lock before creating the directory so
      // there is no race condition with another application trying
      // to clean your temp dir.
      // Save the lock path into ArrayList. This will allow the
      // TempFileManager to clean the overall temp directory next time.
      lockFiles.add(tmpDir.getName()+".lck");

      // Make a temp directory that you will use for all future requests.
      if (!tmpDir.mkdirs())
      {
        throw new IOException("Unable to create temporary directory:"
            + tmpDir.getAbsolutePath());
      }

      sTmpDir = tmpDir;
    }

    sTmpDir.mkdirs();


    // Generate a temp file for the user in your temp directory
    // and return it.
    return File.createTempFile(prefix, suffix, sTmpDir);
  }


  /**
   *  Utility method to load the TempFileManager at any time and allow it to
   *  clean the temporary files that may be left from previous instances
   *
   * @param  args  command line arguments are currently not supported
   */
  public static void main(String[] args)
  {
    // Although the JVM will load the class in order to
    // run the main method, this gives a little clarity to
    // what is happening and why we want the main method.
    try
    {
      // This will load the TempFileManager, which will
      // cause the static block to execute, cleaning
      // any old temp files.
      Class.forName(TempFileManager.class.getName());
    }
    catch (ClassNotFoundException ex)
    {
      ex.printStackTrace();
    }
  }


  /**
   *  Deletes all of the files in the given directory, recursing into any sub
   *  directories found. Also deletes the root directory.
   *
   * @param  rootDir       the root directory to be recursively deleted
   * @throws  IOException  if any file or directory could not be deleted
   */
  private static void recursiveDelete(File rootDir)
    throws IOException
  {
    // Select all the files
    File[] files = rootDir.listFiles();
    for (int i = 0; i < files.length; i++)
    {
      // If the file is a directory, we will
      // recursively call delete on it.
      if (files[i].isDirectory())
      {
        recursiveDelete(rootDir);
      }
      else
      {
        // It is just a file so we are safe to
        // delete it
        if (!files[i].delete())
        {
          throw new IOException("Could not delete: " + files[i].getAbsolutePath());
        }
      }
    }

    // Finally, delete the root directory now
    // that all of the files in the directory have
    // been properly deleted.
    if (!rootDir.delete())
    {
      throw new IOException("Could not delete: " + rootDir.getAbsolutePath());
    }
  }


  /**
   *  The prefix for the temp directory in the system temp directory
   */
  //private static String TEMP_DIR_PREFIX = "tmp-mgr-";
  private final static String TEMP_DIR_PREFIX = AppUtils.getAppRoot();

  /**
   *  The temp directory to generate all files in
   */
  private static File sTmpDir = null;
  private static ArrayList lockFiles = new ArrayList();
  /**
   *  Static block used to clean up any old temp directories found -- the JVM
   *  will run this block when a class loader loads the class.
   */
  static
  {
    try{
      // Clean up any old temp directories by listing
      // all of the files, using a filter that will
      // return only directories that start with your
      // prefix.
      FileFilter tmpDirFilter =
          new FileFilter() {
        public boolean accept(File pathname) {
          return (pathname.isDirectory() &&
                  pathname.getName().startsWith(TEMP_DIR_PREFIX));
        }
      };

      // Get the system temp directory and filter the files.
      String tmpDirName = System.getProperty("java.io.tmpdir");
      File tmpDir = new File(tmpDirName);
      File[] tmpFiles = tmpDir.listFiles(tmpDirFilter);

      // Find all the files that do not have a lock by
      // checking if the lock file exists.
      for (int i = 0; i < tmpFiles.length; i++) {
        File tmpFile = tmpFiles[i];

        // Create a file to represent the lock and test.
        if (!lockFiles.contains(tmpFile.getName() + ".lck")) {
          // Delete the contents of the directory since
          // it is no longer locked.
          Logger.getLogger("default").log(Level.FINE,
                                          "TempFileManager::deleting old temp directory " +
                                          tmpFile);

          try {
            recursiveDelete(tmpFile);
          }
          catch (IOException ex) {
            // You log at a fine level since not being able to delete
            // the temp directory should not stop the application
            // from performing correctly. However, if the application
            // generates a lot of temp files, this could become
            // a disk space problem and the level should be raised.
            Logger.getLogger("default").log(Level.INFO,
                                            "TempFileManager::unable to delete " +
                                            tmpFile.getAbsolutePath());

            // Print the exception.
            ByteArrayOutputStream ostream = new ByteArrayOutputStream();
            ex.printStackTrace(new PrintStream(ostream));

            Logger.getLogger("default").log(Level.FINE, ostream.toString());
          }
        }
      }
    }catch(Exception ex){
      ex.printStackTrace();
    }
  }
}

