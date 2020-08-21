# SkyrimChallengeJson
This software is used for a challenge a friend came up with.
I made this program to give myself some more experience in the language, as well to use it for its' purpouse.

If you want to compile this yourself all you need is the following

--- Visual studio 2019

--- NewtonsoftJson.Net Nuget


This program is made to be configurable by basically anyone.
Whn you first start your program, you might notice 4 new files being created in the json format.
data.json, setupConig.json, alternativeSetupConig.json and characterconfig.json

data.json                   ::: This is some basic program storage, like paths to different files, except the data.json. It will always be the same directory as the executable.
setupConig.json             ::: In here is the configuration to what you want the possible follower locations ,objectives, races and such to be.
alternativeSetupConfig.json ::: This is an alternative, the reason is to show off the rest of the features. Look at the last objective "Kill ebony knight"
characterConfig.json        ::: This is your character. Objectives you have finished an so on.

How to use it.

When you first start the program, all you need to do is press "Reset". Pressing this button will give you a race, a follower location, an objective and the same amount of skills as stated inside the data folder. You will always recieve an attacking skill when you reset.
Next to your active race, there is a little box with the number 0 in it. This number is your level.
Whenever you finish your objective you update your level and press "Objective Finished", this will put your finished objective inside a list of finished objectives, as well as give you the same amount of skills stated inside setupConfig.json for that objective.
You will probably get a window popping up that says "No fast travel", this means that you choose an objective you want to finish, but you are not allowed to use fast travel during this objective. Closing this window will choose one at random, but still count as no fast traveling.

There are 2 combination of 3 letters that sometimes appear after a finished objective.
NFT = No Fast Travel
ITF = Impossible To Finish ::: This means that, if you finish for example "Stormcloaks", you will never be able to get "Imperials", since you can't do both during one playthrough.
