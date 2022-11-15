# M2MT-system
 Model-To-Model Translation system is designed to offer support for creating and maintaining translations between two or more models.

# Angular Tutorial
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
