<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDetail.aspx.cs" Inherits="admin_FileDetail"  
MasterPageFile="~/Master/AdminConsole.master" ValidateRequest="false"
Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <script>
        $(function () {

            $(function () {

                $("#4").show();
                $("#04").html("-");
                $("#42").addClass("present");
                $("#a4").toggle(function (e) {
                    e.preventDefault();
                    $(this).next().hide();
                    $(this).children(".jia").html("+");
                }, function (e) {
                    e.preventDefault();
                    $(this).next().show();
                    $(this).children(".jia").html("-");
                });
            });

        });
    </script>
 
    <script type="text/javascript">

        function bImgInsert_onclick() {
            document.getElementById("UploadImg").style.display = "block";
        }

        function insert() {
            // 上传成功
            if (document.getElementById("UploadImg").contentWindow.document.getElementById("ImgPath").value) {
                var savePath = document.getElementById("UploadImg").contentWindow.document.getElementById("ImgPath").value; //imgpath 呈现为html时候是<input type="text" />，所以用value
                document.getElementById("UploadImg").style.display = "none";
                //写入编辑器
                CKEDITOR.instances.editor1.insertHtml(" <img src='" + savePath + "' />");
            }
        }
    </script>

    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div>
                <asp:Label ID="ErrorMessage" runat="server" Visible="False" Width="912px" 
                    BackColor="Red" ForeColor="White"></asp:Label>
            </div>
            <div>
            <asp:Label ID="TipMessage" runat="Server" Visible="false" Width="912px" BackColor="LightGreen"
            ForeColor="white"></asp:Label></div>
    <div id="news-editor-bg">
    </div>
    <table id="news-editor-table">
        <tr>
            <td>
                文件名
            </td>
            <td>
                 <%=Request.QueryString["FileName"] %>
            </td>
        </tr>   
              <tr>
            <td>
                文件路径:
            </td>
            <td>
                <%=fileUrl%>
            </td>
        </tr> 
        <tr>
        <td >
            所属项目：</td>
        <td><%=projectName %> </td>
        </tr>  
        <td>创建者</td>
        <td><%=userName %></td>
        <tr>
        <td>详细介绍</td></tr>
    </table>
    <div id="news-editor-plug">
        <div>
            <textarea cols="80" id="editor1" name="editor1"><%=description %></textarea>

            <script type="text/javascript">
                CKEDITOR.replace('editor1', { width: 800, height: 320 });
            </script>

        </div>
        <div id="news-insert-picture"><a href="javascript:bImgInsert_onclick();">插入图片</a></div>
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="提交" meta:resourcekey="btnSubmitResource1" />
    </div>
  
      <iframe id="UploadImg" src ="ImgUpload.aspx" style="display:none" height="40" width ="500"  frameborder="0" scrolling="no"  onload="insert()" ></iframe>
    </iframe>
</asp:Content>



    
   
       
     
       
      



