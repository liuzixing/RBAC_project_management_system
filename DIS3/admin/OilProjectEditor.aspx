<%@ Page Title="" Language="C#"ValidateRequest="false"  MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="OilProjectEditor.aspx.cs" Inherits="admin_OilProject" %>
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
    <div id="news-editor-bg">
    </div>
    <table id="news-editor-table">
        <tr>
            <td>
                加油站项目名称:
            </td>
            <td>
                  <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>  
        <tr>
        <td colspan="2">
            <asp:CheckBox ID="cbConfirm" runat="server" Checked="True" TextAlign="Left" text="在前台显示"/>
        </td>
        </tr> 
        <tr>
        <td>详细介绍</td></tr>
    </table>
    <div id="news-editor-plug">
        <div>
            <textarea cols="80" rows="auto" id="editor1" name="editor1"><%=editorContent %></textarea>

            <script type="text/javascript">
                CKEDITOR.replace('editor1', { width: 800, height: 320 });
            </script>

        </div>
        <div id="news-insert-picture"><a href="javascript:bImgInsert_onclick();">插入图片</a></div>
        <asp:Button ID="Button2" runat="server" OnClick="btnSubmit_Click" Text="提交" meta:resourcekey="btnSubmitResource1" />
    </div>
      <iframe id="UploadImg" src ="ImgUpload.aspx" style="display:none" height="40" width ="500"  frameborder="0" scrolling="no"  onload="insert()" ></iframe>
    </iframe>
</asp:Content>


