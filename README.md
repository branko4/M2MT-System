# M2MT-system
 Model-To-Model Translation system is designed to offer support for creating and maintaining translations between two or more models.

# Setup Angular

## Project setup
Angular supports the modular architecture pattern. This is both as by having [modules](https://angular.io/guide/ngmodules), as well as having [libraries](https://angular.io/guide/libraries).

### Applications
The current project contains the following applications:
- Mapping; This is the application that can be used to make and view a given mapping.

#### Mapping application setup
The mapping application contains two modules. These are the default app module and the module required for routing. The application has several components and services. Therefore they are separated into two directories (`components` and `service`). The components directory is separated into two directories. One directory is for components that are representing a page (pop-up included), which is the `screen-components` subdirectory. While the other directory have components, that are added and shared between multiple pages. This is the `element-component` directory. Note that if a component is only used by one screen component it is in the subdirectory of that component and not in the `element-component` directory.

##### special classes
The biggest part of the software is inline with Angular, however some parts are going further then Angular. One of these classes is the `MappingService` class. Although it is still an Angular service, there are something added, to keep it better manageable. First of all it is a little bit in a gray area. Since official the components are for visualization and services are there to do the DRY work. However since this is the local app state and changes to the app state means that the mapping service needs to change behavior and the view needs to look different or better said needs to enable specific task depending to the state, the exception has been made to handle both the app state, as well as the visualization of it.

###### MappingService
The Mapping service uses state pattern. Where each activity in the entire state diagram is a function of a state interface and each state is a class that implements the state interface. Therefore the behavior can be easily managed and verified. Since each state behavior is written in it's own class. This will also make the use of state if statements redundant. [State pattern on Wikipedia](https://en.wikipedia.org/wiki/State_pattern)

The implementation of the state pattern is in this case only done halfway, since the state can be changed from outside of the state pattern, however there is only one who holds the state, which is the MappingService. The state transition are done both through an observer pattern a like solution [rx library](https://angular.io/guide/rx-library) and by the mapping service forcing a state transition . By creating a new object and setting the state. The observer pattern alike solution changes the state by given a change event. However the MappingService is still response for changing the state, since it follows the observable.

This could be made more neat by creating a generic State Manager, which takes a generic type state (interface). State interface could extend a `StateInterface`, which has the function `setStateManager(stateManager: StateManagerChanger)`. Where the state manager implements the `StateManagerChanger` interface. The `StateManagerChanger` interface has a function `setCurrentState(stateInterface: StateInterface)`. The implementation will change the current reference to the state. This will make it possible to give the state full access to update the state while the state doesn't know its real container.


**Small side note; *Just though about the fact that there will be a problem with reaching the functions, a solution needs to be found***

### Libraries
The current project contains the following libraries:
- Shared; This is a library that contains more generic components, like tables.

#### Shared library setup
The shared library contains only one module. This module exports several components. The following components are exported by this module:
- conformation-pop-up
- pop-up
- stepper
- table

##### Usage
See mapping application, however it should be as simple as importing the module into the app module. When that is done you should reference the component you want to use.

**Don't forget to prefix the component with lib, when referencing them in a template.**

###### pop-up
To use the pop-up add the content between the pop-up tags.

Outputs:
- backgroundClick; this event will be emitted when the background of the background is clicked

Example:
```HTML
<lib-pop-up>
  <!-- Some html like content -->
</lib-pop-up>
```

###### stepper
This component can be used to move between steps c.q. actions like a create account could have steps like contact info, email conformation, telephone conformation, etc.

**Please note that this component uses some more advanced Angular and TS code, this will not be documented**

Inputs:
- steps: *Step\<any\>*[]; This is a list of steps. A Step is a self defined interface, which require a reference to the component and the data that is required by the component. Since each component might need different data, this is a generic type. However to make sure the data is still correct the step should be created through either a specific function for creating the step or direct reference with proper typing.

Outputs:
- stepsChange: *EventEmitter\<Step\<any\>*[]*\>*; This event is emitted each time a step moves to a different step.

To create a step component you need to extant the step component interface.

###### **Example code:**
**HTML of user**
```HTML
<lib-stepper [(steps)]="steps" (stepsChange)="onFormDataChanged()"></lib-stepper>
```
**TS of user**
```TS
class UserOfStepper {

  steps: Step<any>[] = [
    SelectElementComponent.GetReference({
      input: {
        modelRef: {
          id: "dfgb",
          id2: "dfgb"
        },
      },
      output: {},
    }),
    SelectElementComponent.GetReference({
      input: {
        modelRef: {
          id: "EULYNX",
          id2: "EULYNX"
        },
      },
      output: {},
    }),
    MappingRuleMetaDataComponent.GetReference({
      output: {},
    }),
  ];
}
```


**TS of a step**
```TS
interface ExpectedData {
  // some data
}

class SelectElementComponent implements StepComponent<ExpectedData> {
  @Output() selected = new EventEmitter<{name: string}>()
  data?: ExpectedData;

  static GetReference(data: ExpectedData): Step<ExpectedData> {
    return {
      component: SelectElementComponent,
      data: data,
    };
  }
}
```

###### table
The table is rather simple compart to the stepper.
The table consists out of standard columns and one action columns.

Input:
- data: *Table*; This input is both the (labeling of) columns, rows (actual data), and optional the actions, when actions list is empty, the action column is not shown.

Just follow the given data definition and you will probably be fine. However, the action column might be a bit more difficult.

A action is just a callback function, which means that when an action class is maded, the action class will be the reference environment, since this one will call, c.q. create the function. Therefore the `that` variable is given. Which is a reference to the previous referencing environment.

So to short up. An Action requires three arguments. The first is the name of the action. The second one is a function. The function will be called when the button is pressed. The final one is the reference environment, of which you want the function to have access to.

One better solution would be to replace the function by an observer like solution, for instance using ng rx might be better or to create observer and observable, which is kind a the that variable of the creation.


# Setup backend

## !**Important**!
- Since the backend uses npgsql, don't forget to add/ change the connections string in the `test-env.json` and `appsettings.Development.json`. Which includes:
  - password
  - location
  - username
  - database, since database, if not defined, is equal to username

# Angular Tutorial ***For now discontinued***
tk

## Structure of component
A component consist out of four parts. These are:
- A TypeScript (TS) part
- A Template part
- A style part
- A Test part

It is neat to put every part in their own file, however strictly speaking it should be possible to just put everything in a single file, but that is not the recommended option. The template part will be a HTML template for 99% of the time, however it is at least possible to also use a SVG as a template. It is possible to use several different style types, for instance the classic CSS is supported, but it is also supported to use SCSS and at least two more.

To add a component manually the following steps are required:
- Create the component TS file
- Add and export a class  
- Annotate the class with the Component annotation, don't forget to import the annotation.
- Reference the template file or add the template code inside the annotation
- Do the same for your styling file
- Add the component to the app module (there are more options then only app module, however for now this is the best option)

Your component should look something like:
```TS
import { Component } from '@angular/core';

@Component({
  selector: <name to use in template>,
  templateUrl: <reference to template>,
  styleUrls: [<reference to style sheet(s)>]
})
export class <class name> { }
```
Most of the variable places will speak for them self, however just to be sure:
- <name to use in template>; is the name you want to give the component, the way you will refer to the component from other templates.
- <reference to template>; is a path to the template file, including filename.
- <reference to style sheet(s)>; is one ore more paths to the style sheets you want to apply to this component.
- <class name>; is the name you will use to refer to this component from other TS files.

The app module should be edited so the declarations list contains a reference to the component (type). Which means that the component should **not** be initialize. Don't forget to import the component in this TS file.

### Value from code to html
tk

### Directives
<!-- add something about ngIf ngFor -->
tk

### Event binding

## Commandos
Since most of the previous is always the same, there is also a commando available. In fact for the creation of almost all Angular tk there is a commando available. The commando will probably always start with ng followed by the commando. For initialize a project you could use `ng new <app name>`. This will get you through the setup of a new Angular project. This commando can be quite basic, but it can also be quite advanced. Most of the time the basic is good enough. However for larger projects the advanced options can be helpful. To use the advance options use the `--help` tag by the commando and/or read the [official documentation](https://angular.io/docs).
For most of the other generations you can use the `ng generate <tk> <name>`. The tk can be one of the specified constructs. The name is just the name you want the tk to have. For instance if you want to generate a component with the name example the following commando should be ran: `ng generate component example`. Since most developers has become a developer, cause they are lazy 😇, there is also a short hand commando, which is `ng g c example`, where the g is for generate and the c is for component. Most of the other tk have also a tk, for instance for a service it is a `s`. by adding the `--help` tag to the `ng g` commando you will get a list with tk and there aliases.

It is also handy to know that it is possible to create components in a subdirectory of another component. This can be done by adding the component, of which the new component should be in the subdirectory, before the new component. Since the previous sentence probably makes no sense take the following example:
You want a new component, called basic, to be added in the subdirectory of the example component (or just example directory). Which means you have to use the following command: `ng g c example/basic`.

## Structure of a basic angular project
After generating an Angular project you will most likely have the following project structure:
- src
  - app
    - app-routing.module.ts
    - app.component.html
    - app.component.scss
    - app.component.spec.ts
    - app.component.ts
    - app.module.ts
  - assets
  - environments
  - some other lesser important files, however you still should take a look at it

The we will ignore the app-routing.module.ts for now. However you probably recongise the app.module.ts and the app.component.\*. The app.component.\* is indeed a component however on default this will be marked as the root component, the component will be the parent of all other components. So when you are starting a new project this is where you most likely will start.

The app.module.ts is the main module for your app. Each component you use should be specified here. There will be a better look at the module later on.

Besides the app module and component there is a folder for assets, which you could put your assets in, like static images, like logos. The environments folder contains two files. You might be familiar with environment variables. Consider the files in this folder like those variables, in fact they are environment variables. However unlike system environment variables, which you could access and change during runtime, these variables are set during compile time. Or at least to my understanding they are.
The highest pro of using the environment file is that you can change locations between a development and a production environment. Which means you could use a localhost or a test server during development and during production you could use a domain name or the official address.
Another pro is that you could set backend addresses, which might change overtime, which means you only have to change them in the environment file and not through the entire application.

There are also a lot of other files and directories, which you should take a look at, like favicon.ico, index.html, styles.scss and some others. However these file properly speak for them self or it would be going to deep to early.

One important file, which I will not go into detail with, is the angular.json. These file is the project configuration file, which you can end up with to change somethings, like to add more then just one asset folder, or to change a location, since the path has changed for whatever reason. The exact usage of this file is not important - for now - however, you might want to keep this file in mind, since it needs to be changed from time to time. As specially with multi library/ application projects, like the M2MT project is.

## Linking of components
Now we have run through the basics. Lets get started with building stuff. Most of the time you want to have multiple components inside a component and you want to splits big components into smaller ones, so you can reuse parts of your view.
Take for instance a login component, which you want to show to the user on your main page when the user is not logged in. However when the user is on any other page you might want it to appear in a pop up or just on it's own page. When the login part is separated into a component and the component is build correctly it could be possible to reuse the behavior of the login component and the visual layout of the login, like the input fields and the login button. This should make it cheaper to reuse a component and or to change the layout of a page.

a normal part of a HTML page might look something like:
```html
<p>
  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
</p>
<form>
  <label for="username">username</label>
  <input name="username"/>
  <label for="password">password</label>
  <input name="password" type="password"/>
  <input type="submit" value="login"/>
</form>
```

Now instead of doing it like the previous example. You should create a login component. Which might get the selector app-login. Then your component where the page above is shown will look like the following:
```html
<p>
  Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
</p>
<app-login></app-login>
```

And the app login html template will look like:
```html
<form>
  <label for="username">username</label>
  <input name="username"/>
  <label for="password">password</label>
  <input name="password" type="password"/>
  <input type="submit" value="login"/>
</form>
```

However by forms Angular could do more then this, but lets keep it simple.

### Input
In some cases you might want to give a component some extra data. You could do that by databinding.
tk

### Output
### IO combination
## Services
### What is a service
### How to link a component
#### What is Dependency Injection (DI)
#### How does DI work in angular
## Communicating with backend c.q. using HTTP(S) protocol
## Using modules
### Modular architecture
### Why?
### Modules
### Libraries
## Routing
## What next?
## Assignments
### Setup a new project
### Add a component
### Edit the component
### Combine components
### Add a service
### connect a component with a service
### Connect a service with a backend (mock)
### Result
