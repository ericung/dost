# Dev Ops Speech to Text

The Dev Ops Speech to Text, dost, this project aims to execute command line interfaces from voice to help aid in the process of dev ops.

## Challenges in this project
One of the goals is to convert the commands into more user friendly commands.\
Ex. cd <FOLDER> = change directory to <FOLDER>\
Ex. az costmanagement export <...> = az costmanagement export <...>\
The challenging part is putting specifics in such as API keys and passwords.\
This is an interesting problem that can have many variations in solutions.\
Another challenge would be separating out windows and linux commands.

## Design of Commands
### Mapping CLI commands to custom user friendly commands
Just because it's simple doesn't mean it's easy.
![CLI To Custom Commands](Resources/systemdesign01.png)
![Templating](Resources/systemdesign02.png)

-----

if thou dost love me, for love is free
