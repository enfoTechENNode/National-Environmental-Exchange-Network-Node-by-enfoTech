package Node.API;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Properties;
import java.util.Enumeration;
import java.util.TimeZone;
import java.io.InputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.io.FileOutputStream;
import java.io.FileInputStream;

import Node.Phrase;

/**
 * <p>API for generating a version property file</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * 
 * @author enfoTech
 * @version 2.0
 */
public class PropertyFiles {

	public static Properties createDefaultProperties() {

        Properties tempProp = new Properties();

        tempProp.setProperty(Phrase.buildDate, GetSysTimeString("yyyy-MM-dd HH:mm"));
        return tempProp;

    }


	public static void printProperties(Properties p, String s) {

        System.out.println();
        System.out.println("========================================");
        System.out.println(s);
        System.out.println("========================================");
        System.out.println("+---------------------------------------+");
        System.out.println("| Print Application Properties          |");
        System.out.println("+---------------------------------------+");
        p.list(System.out);
        System.out.println();
    }


    public static void saveProperties(Properties p, String fileName) {

        OutputStream propsFile;

        try {
            propsFile = new FileOutputStream(fileName);
            p.store(propsFile, "Properties File.");
            propsFile.close();
        } catch (IOException ioe) {
            ioe.printStackTrace();
        }

    }


    public static Properties loadProperties(String fileName) {

        InputStream propsFile;
        Properties tempProp = new Properties();

        try {
            propsFile = new FileInputStream(fileName);
            tempProp.load(propsFile);
            propsFile.close();
        } catch (IOException ioe) {
            ioe.printStackTrace();
        }

        return tempProp;

    }


    public static Properties alterProperties(Properties p) {

        Properties newProps = new Properties();
        Enumeration enProps = p.propertyNames();
        String key = "";

        while ( enProps.hasMoreElements() ) {

            key = (String) enProps.nextElement();
            
            // if going to change key add more if below
            if (key.equals("buildDate")) {
            	String value = GetSysTimeString("yyyy-MM-dd HH:mm");
            	
                newProps.setProperty(key, value);
            } else {
                newProps.setProperty(key, p.getProperty(key));
            }

        }

        return newProps;

    }


    /**
     * Sole entry point to the class and application.
     * @param args Array of String arguments.
     */
    public static void main(String[] args) {

        //final String PROPFILE= "bin/ApplicationResources.properties";
        // for ant build must change the path
        final String PROPFILE= "../../../Node/bin/ApplicationResources.properties";
        Properties myProp;
        Properties myNewProp;

        myProp = createDefaultProperties();
        //printProperties(myProp, "Newly Created (Default) Properties");
        //saveProperties(myProp, PROPFILE);
        myNewProp = loadProperties(PROPFILE);
        printProperties(myNewProp, "Loaded Properties");
        myNewProp = alterProperties(myProp);
        printProperties(myNewProp, "After Altering Properties");
        saveProperties(myNewProp, PROPFILE); 

    }
    
	public static String GetSysTimeString (String format)
	{
		Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		SimpleDateFormat f = new SimpleDateFormat(format);
		String temp = f.format(cal.getTime());
		return temp;
	}

}
