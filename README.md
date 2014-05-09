What is Basco ?
===============
Basco is a simple and easy testable state machine.  
It was specially designed for usage in TDD environments.

How do I get started ?
======================
1. Install **Basco** Nuget package  
2. Create your state machine classes
	1. Define transitions (*enum*)  
    2. Create all states. Derive from IState.  
		If needed implement also IStateEnter and ***IStateExit***
    3. Implement the transition configurator (derived from ***IBascoConfigurator***)
3. Create binding module (see Ninject module)
4. Inject IBasco where you use the state machine and start it (*Start()*)
5. Have fun :-)   

Where can I get it ?
====================
First, install NuGet.  
Then, install Basco from the package manager console:

`PM> Install-Package Basco`  

