import _ from 'lodash-es';

export default class EmployeesCollection {

    constructor(employees) {
        this.employees = employees;
    }

    addEmployee(employee) {
        this.employees.push(emp);
    }

    sort() {
        return _.sortBy(this.employees, ['salary', 'name']);
    }

    getFirstNames(quantity) {
        if (!quantity || typeof(quantity) !== "number") quantity = 5;
        return _.slice(this.employees, 0, quantity)
    }

    getLastIds(quantity) {
        if (!quantity || typeof(quantity) !== "number") quantity = 3;
        return _.slice(_.reverse(_.cloneDeep(this.employees)), 0, quantity);
    }
}
