using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///WebFile 的摘要说明
/// </summary>
public class WebFile
{
	public WebFile()
	{
		
	}
	public int FileID { get; set; }  //none when upload
	// 文件名 FileName!=NULL and include the extension(.rar/.zip)
    public string FileName { get; set; }
	// 文件存储路径，相对用户目录
    public string FileDirectory { get; set; }
	// 备注
	public string Description { get; set; }
	// 拥有者用户名，前台显示时用到  none when upload
	public string OwnerName { get; set; }
	// 上传者用户名，前台显示时用到  none when upload
	public string UploaderName { get; set; }
	// 拥有者ID
	public int OwnerID { get; set; }
	// 上传者ID
	public int UploaderID { get; set; }
	// 上传时间
	public DateTime CreateTime { get; set; }
	// 公开文档 或 私有文档
	public WebFileType FileType { get; set; }
    // 文件大小
    public long FileSize {get; set;}
}

public enum WebFileType
{ 
	Public,
	Private
}