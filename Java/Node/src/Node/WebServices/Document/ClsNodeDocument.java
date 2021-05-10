package Node.WebServices.Document;

/**
 * This class is used to represent a node document that is used to be sent via
 * soap attachments for web service requests.
 * <p>
 * Company: enfoTech & Consulting, Inc.
 * </p>
 * 
 */
public class ClsNodeDocument {
	private String id;
	private String name;
	private String type;
	private byte[] content;

	/**
	 * Constructs a Node Document.
	 */
	public ClsNodeDocument() {
	}

	/**
	 * @return the id
	 */
	public String getId() {
		return id;
	}

	/**
	 * @param id the id to set
	 */
	public void setId(String id) {
		this.id = id;
	}

	/**
	 * Get Document Name.
	 * 
	 * @return document name.
	 */
	public String getName() {
		return name;
	}

	/**
	 * Set Document Name.
	 * 
	 * @param newName
	 *            name of document.
	 */
	public void setName(String newName) {
		name = newName;
	}

	/**
	 * Get Document Type.
	 * 
	 * @return document type.
	 */
	public String getType() {
		return type;
	}

	/**
	 * Set Document Type.
	 * 
	 * @param newType
	 *            type of document.
	 */
	public void setType(String newType) {
		type = newType;
	}

	/**
	 * Get Content of Document.
	 * 
	 * @return content in byte[] format.
	 */
	public byte[] getContent() {
		return content;
	}

	/**
	 * Set Content of Document.
	 * 
	 * @param newContent
	 *            content in byte[] format.
	 */
	public void setContent(byte[] newContent) {
		content = newContent;
	}
}
