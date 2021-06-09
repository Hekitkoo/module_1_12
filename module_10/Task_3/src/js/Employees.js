export default class Employee {
    constructor(id, name, salary) {
        this.id = id;
        this.name = name;
        this.salary = salary;
    }
}

export default class FixedSalaryEmployee extends Employee {
    constructor(id, name, salary) {
        super(id, name, salary);
    }

    getSalary() {
        return this.salary;
    }
}

export default class HourlySalaryEmployee extends Employee {
    constructor(id, name, salary) {
        super(id, name, salary);
    }

    getSalary() {
        return this.salary * 20.8 * 8;
    }
}

