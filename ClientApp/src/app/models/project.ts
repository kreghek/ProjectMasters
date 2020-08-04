


    export class Project {  
        constructor(public id: number, public name: string, public features: Feature[]) {  
        }  
    }  

    export class Feature {  
        constructor(public id: number, public name: string, public tasks: FeatureTask[]) {  
        }  
    }  

    export class FeatureTask {  
        constructor(public id: number, public name: string, public requiredSkills: SkillScheme[], public blockers: FeatureTask[], public progress: number, public realValue: number, public estimate: number, public logged: number, public assignees: Employee[]) {  
        }  
    }  

    export class Employee {  
        constructor(public id: number, public name: string, public skills: Skill[]) {  
        }  
    }  

    export class Skill {  
        constructor(public id: number, public scheme: SkillScheme, public value: number) {  
        }  
    }  

    export class SkillScheme {  
        constructor(public id: number, public name: string) {  
        }  
    }  

    export class Player {  
        constructor(public id: number, public energy: number, public projects: Project[]) {  
        }  
    }  
