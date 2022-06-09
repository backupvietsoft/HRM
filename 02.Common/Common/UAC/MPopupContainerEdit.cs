using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Popup;

namespace Commons
{
    [UserRepositoryItem("RegisterMPopupContainerEdit")]
    public class RegisterMPopupContainerEdit : RepositoryItemPopupContainerEdit
    {

        static RegisterMPopupContainerEdit() { RegisterMPopupContainerEdit1(); }
        public const string CustomEditName = "MPopupContainerEdit";
        public RegisterMPopupContainerEdit() { }
        public override string EditorTypeName { get { return CustomEditName; } }
        public static void RegisterMPopupContainerEdit1()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(
                CustomEditName,
                typeof(MPopupContainerEdit),
                typeof(RegisterMPopupContainerEdit),
                typeof(MPopupContainerEdit1ViewInfo),
                new ButtonEditPainter(),
                true,
                img));
        }



        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override PopupContainerControl PopupControl
        {
            get { return base.PopupControl; }
            set { base.PopupControl = value; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override int ActionButtonIndex
        {
            get { return base.ActionButtonIndex; }
            set { base.ActionButtonIndex = value; }
        }



        public int DefaultActionButtonIndex { get; set; }
        public PopupContainerControl DefaultPopupControl { get; set; }



        public int DifferentActionButtonIndex { get; set; }
        public PopupContainerControl DifferentPopupControl { get; set; }


        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RegisterMPopupContainerEdit source = item as RegisterMPopupContainerEdit;
                if (source == null) return;

                DefaultActionButtonIndex = source.DefaultActionButtonIndex;
                DefaultPopupControl = source.DefaultPopupControl;

                DifferentPopupControl = source.DifferentPopupControl;
                DifferentActionButtonIndex = source.DifferentActionButtonIndex;
            }
            finally
            {
                EndUpdate();
            }
        }
    }
    public class MPopupContainerEdit : PopupContainerEdit
    {
        #region Some default stuff for custom edit (constructors, registration, etc).
        static MPopupContainerEdit() { RegisterMPopupContainerEdit.RegisterMPopupContainerEdit1(); }
        public MPopupContainerEdit() { }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RegisterMPopupContainerEdit Properties { get { return base.Properties as RegisterMPopupContainerEdit; } }
        public override string EditorTypeName { get { return RegisterMPopupContainerEdit.CustomEditName; } }
        #endregion

        protected override bool IsActionButton(EditorButtonObjectInfoArgs buttonInfo)
        {
            int buttonIndex = Properties.Buttons.IndexOf(buttonInfo.Button);

            if (buttonIndex == Properties.DefaultActionButtonIndex ||
                buttonIndex == Properties.DifferentActionButtonIndex)
            {
                //Set the Properties.ActionButtonIndex value according to which button is pressed:
                Properties.ActionButtonIndex = buttonIndex;

                //Set the Properties.PopupControl according to which button is pressed:
                if (buttonIndex == Properties.DefaultActionButtonIndex)
                    Properties.PopupControl = Properties.DefaultPopupControl;
                else
                    Properties.PopupControl = Properties.DifferentPopupControl;

                return true;
            }

            return false;
        }
    }
    public class MPopupContainerEdit1ViewInfo : PopupContainerEditViewInfo
    {
        public MPopupContainerEdit1ViewInfo(RepositoryItem item) : base(item) { }

        public new RepositoryItemPopupBase Item { get { return base.Item as RegisterMPopupContainerEdit; } }

        //Show the pressed state when button is pressed or when popup is open.
        protected override bool IsButtonPressed(EditorButtonObjectInfoArgs info)
        {
            var hitObject = PressedInfo.HitObject as EditorButtonObjectInfoArgs;

            return
                (hitObject != null && hitObject.Button == info.Button) ||
                (IsPopupOpen && Item.ActionButtonIndex == info.Button.Index);
        }
    }
}
