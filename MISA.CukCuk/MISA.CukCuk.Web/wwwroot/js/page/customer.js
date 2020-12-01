$(document).ready(function () {
  new CustomerJS();
});

class CustomerJS extends BaseJS {
  constructor() {
    super()
    this.router = "customer";
    this.loadData();
  }
}