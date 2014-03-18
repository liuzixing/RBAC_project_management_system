<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/Master/AdminConsole.master" 
AutoEventWireup="true" CodeFile="PlanEditor.aspx.cs" Inherits="admin_PlanEditor"  
Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">

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
    <script>
        $(function () {
            $("#6").show();
            $("#06").html("-");
            $("#61").addClass("present");
            $("#a6").toggle(function (e) {
                e.preventDefault();
                $(this).next().hide();
                $(this).children(".jia").html("+");
            }, function (e) {
                e.preventDefault();
                $(this).next().show();
                $(this).children(".jia").html("-");
            });
        });
     </script>

    <script type="text/javascript" src="../ckeditor/ckeditor.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div id="news-editor-bg">
    </div>
    <table id="news-editor-table">
        <tr>
            <td>
                计划类型:
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                计划标题:
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="271px" Height="18px" meta:resourcekey="txtTitleResource1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                作者:
            </td>
            <td>
                <asp:TextBox ID="txtAuthor" runat="server" Height="20px" Width="125px" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td>计划内容编辑</td></tr>
    </table>
    <div id="news-editor-plug">
        <div>
            <textarea cols="80" id="editor1" name="editor1"><%=editorContent %></textarea>

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

