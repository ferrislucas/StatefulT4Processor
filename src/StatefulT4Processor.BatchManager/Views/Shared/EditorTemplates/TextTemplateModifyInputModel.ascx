<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StatefulT4Processor.TextTemplateBatchManager.Models.TextTemplateModifyInputModel>" %>

        <fieldset>
			<%: Html.HiddenFor(model => model.Id) %>

            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.ZipFile) %>
            </div>
            <div class="editor-field">
				<input type="file" name="ModifyInputModel_ZipFile" /> <%=Model.ZipFilename %>
				<%: Html.HiddenFor(model => model.ZipFilename) %>
                <%: Html.ValidationMessageFor(model => model.ZipFile) %>
            </div>
        
		</fieldset>