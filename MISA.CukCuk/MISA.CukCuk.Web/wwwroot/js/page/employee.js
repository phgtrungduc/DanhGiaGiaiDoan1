$(document).ready(function () {
  new EmployeeJS();
});

class EmployeeJS extends BaseJS {
  constructor() {
    super();
    this.router = "employee";
    this.loadData();
  }
}
