﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PWSZ_WCF.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LessonPlan", Namespace="http://schemas.datacontract.org/2004/07/BazusApi")]
    [System.SerializableAttribute()]
    public partial class LessonPlan : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private PWSZ_WCF.ServiceReference1.Lesson[] LessonsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public PWSZ_WCF.ServiceReference1.Lesson[] Lessons {
            get {
                return this.LessonsField;
            }
            set {
                if ((object.ReferenceEquals(this.LessonsField, value) != true)) {
                    this.LessonsField = value;
                    this.RaisePropertyChanged("Lessons");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Lesson", Namespace="http://schemas.datacontract.org/2004/07/BazusApi")]
    [System.SerializableAttribute()]
    public partial class Lesson : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BuildingField;
        
        private System.DateTime EndTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FieldOfStudyIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FieldOfStudyNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GroupNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InstructorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LessonIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LessonNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LessonTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RecruitmentYearField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RoomNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecialtyIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecialtyNameField;
        
        private System.DateTime StartTimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Building {
            get {
                return this.BuildingField;
            }
            set {
                if ((object.ReferenceEquals(this.BuildingField, value) != true)) {
                    this.BuildingField = value;
                    this.RaisePropertyChanged("Building");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.DateTime EndTime {
            get {
                return this.EndTimeField;
            }
            set {
                if ((this.EndTimeField.Equals(value) != true)) {
                    this.EndTimeField = value;
                    this.RaisePropertyChanged("EndTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FieldOfStudyId {
            get {
                return this.FieldOfStudyIdField;
            }
            set {
                if ((object.ReferenceEquals(this.FieldOfStudyIdField, value) != true)) {
                    this.FieldOfStudyIdField = value;
                    this.RaisePropertyChanged("FieldOfStudyId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FieldOfStudyName {
            get {
                return this.FieldOfStudyNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FieldOfStudyNameField, value) != true)) {
                    this.FieldOfStudyNameField = value;
                    this.RaisePropertyChanged("FieldOfStudyName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string GroupName {
            get {
                return this.GroupNameField;
            }
            set {
                if ((object.ReferenceEquals(this.GroupNameField, value) != true)) {
                    this.GroupNameField = value;
                    this.RaisePropertyChanged("GroupName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Instructor {
            get {
                return this.InstructorField;
            }
            set {
                if ((object.ReferenceEquals(this.InstructorField, value) != true)) {
                    this.InstructorField = value;
                    this.RaisePropertyChanged("Instructor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int LessonId {
            get {
                return this.LessonIdField;
            }
            set {
                if ((this.LessonIdField.Equals(value) != true)) {
                    this.LessonIdField = value;
                    this.RaisePropertyChanged("LessonId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LessonName {
            get {
                return this.LessonNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LessonNameField, value) != true)) {
                    this.LessonNameField = value;
                    this.RaisePropertyChanged("LessonName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LessonType {
            get {
                return this.LessonTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.LessonTypeField, value) != true)) {
                    this.LessonTypeField = value;
                    this.RaisePropertyChanged("LessonType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RecruitmentYear {
            get {
                return this.RecruitmentYearField;
            }
            set {
                if ((this.RecruitmentYearField.Equals(value) != true)) {
                    this.RecruitmentYearField = value;
                    this.RaisePropertyChanged("RecruitmentYear");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RoomNumber {
            get {
                return this.RoomNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.RoomNumberField, value) != true)) {
                    this.RoomNumberField = value;
                    this.RaisePropertyChanged("RoomNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecialtyId {
            get {
                return this.SpecialtyIdField;
            }
            set {
                if ((object.ReferenceEquals(this.SpecialtyIdField, value) != true)) {
                    this.SpecialtyIdField = value;
                    this.RaisePropertyChanged("SpecialtyId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecialtyName {
            get {
                return this.SpecialtyNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SpecialtyNameField, value) != true)) {
                    this.SpecialtyNameField = value;
                    this.RaisePropertyChanged("SpecialtyName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.DateTime StartTime {
            get {
                return this.StartTimeField;
            }
            set {
                if ((this.StartTimeField.Equals(value) != true)) {
                    this.StartTimeField = value;
                    this.RaisePropertyChanged("StartTime");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IBazusApi")]
    public interface IBazusApi {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBazusApi/GetDataLessonPlan", ReplyAction="http://tempuri.org/IBazusApi/GetDataLessonPlanResponse")]
        PWSZ_WCF.ServiceReference1.LessonPlan GetDataLessonPlan(string indexNumber, System.DateTime dataPlanu);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBazusApi/GetDataLessonPlan", ReplyAction="http://tempuri.org/IBazusApi/GetDataLessonPlanResponse")]
        System.Threading.Tasks.Task<PWSZ_WCF.ServiceReference1.LessonPlan> GetDataLessonPlanAsync(string indexNumber, System.DateTime dataPlanu);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBazusApi/GetDataLessonPlanForFuture", ReplyAction="http://tempuri.org/IBazusApi/GetDataLessonPlanForFutureResponse")]
        PWSZ_WCF.ServiceReference1.LessonPlan GetDataLessonPlanForFuture(string indexNumber, System.DateTime dataPlanu, int dayOfFuture, string teacherName, string teacherSurname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBazusApi/GetDataLessonPlanForFuture", ReplyAction="http://tempuri.org/IBazusApi/GetDataLessonPlanForFutureResponse")]
        System.Threading.Tasks.Task<PWSZ_WCF.ServiceReference1.LessonPlan> GetDataLessonPlanForFutureAsync(string indexNumber, System.DateTime dataPlanu, int dayOfFuture, string teacherName, string teacherSurname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBazusApi/Test", ReplyAction="http://tempuri.org/IBazusApi/TestResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PWSZ_WCF.ServiceReference1.LessonPlan))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PWSZ_WCF.ServiceReference1.Lesson[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(PWSZ_WCF.ServiceReference1.Lesson))]
        object Test();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBazusApi/Test", ReplyAction="http://tempuri.org/IBazusApi/TestResponse")]
        System.Threading.Tasks.Task<object> TestAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBazusApiChannel : PWSZ_WCF.ServiceReference1.IBazusApi, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BazusApiClient : System.ServiceModel.ClientBase<PWSZ_WCF.ServiceReference1.IBazusApi>, PWSZ_WCF.ServiceReference1.IBazusApi {
        
        public BazusApiClient() {
        }
        
        public BazusApiClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BazusApiClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BazusApiClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BazusApiClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PWSZ_WCF.ServiceReference1.LessonPlan GetDataLessonPlan(string indexNumber, System.DateTime dataPlanu) {
            return base.Channel.GetDataLessonPlan(indexNumber, dataPlanu);
        }
        
        public System.Threading.Tasks.Task<PWSZ_WCF.ServiceReference1.LessonPlan> GetDataLessonPlanAsync(string indexNumber, System.DateTime dataPlanu) {
            return base.Channel.GetDataLessonPlanAsync(indexNumber, dataPlanu);
        }
        
        public PWSZ_WCF.ServiceReference1.LessonPlan GetDataLessonPlanForFuture(string indexNumber, System.DateTime dataPlanu, int dayOfFuture, string teacherName, string teacherSurname) {
            return base.Channel.GetDataLessonPlanForFuture(indexNumber, dataPlanu, dayOfFuture, teacherName, teacherSurname);
        }
        
        public System.Threading.Tasks.Task<PWSZ_WCF.ServiceReference1.LessonPlan> GetDataLessonPlanForFutureAsync(string indexNumber, System.DateTime dataPlanu, int dayOfFuture, string teacherName, string teacherSurname) {
            return base.Channel.GetDataLessonPlanForFutureAsync(indexNumber, dataPlanu, dayOfFuture, teacherName, teacherSurname);
        }
        
        public object Test() {
            return base.Channel.Test();
        }
        
        public System.Threading.Tasks.Task<object> TestAsync() {
            return base.Channel.TestAsync();
        }
    }
}
