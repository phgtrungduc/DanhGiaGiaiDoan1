$(document).ready(function () {
  new EmployeeJS();
});

class EmployeeJS extends BaseJS {
  constructor() {
    super();
    this.router = "employee";
    this.loadData();
  }
  mappingData(fieldName,data){
    if (fieldName==="Gender"){
      if (data===0){
        return "Nữ";
      }
      else if (data===1){
        return "Nam";
      }
      else {
        return "Khác";
      }
    }
    if (fieldName==="WorkStatus"){
      if (data===0){
        return "Đang làm việc";
      }
      else if (data===1){
        return "Đang thử việc";
      }
      else {
        return "Đã nghỉ việc";
      }
    }
  }
  customFormat(fieldName,data){
    if (fieldName==="Gender"||fieldName==="WorkStatus"){
      return this.mappingData(fieldName,data);
    }
    else return data;
  }
}
