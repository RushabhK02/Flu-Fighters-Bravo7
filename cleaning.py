import os, json
import xlsxwriter

class LifeLost:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

class DoctorSwitch:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

class PlayerFell:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

class GameStatistics:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

class AudioValue:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

class HumanSwitch:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

class AudioSlider:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

class StoreItemBought:
    def __init__(self, obj, ts):
        self.dict = {}
        self.ts = ts
        if obj is None:
            return
        for key in obj.keys():
            # print(key)
            self.dict[key] = obj[key]

if __name__ == "__main__":
    path_to_json = 'C:\\Users\\rushk\\Desktop\\CS books\\CSCI 526\\Analytics Script\\json'
    json_files = [pos_json for pos_json in os.listdir(path_to_json) if pos_json.endswith('.json')]
    gameStats = []
    livesLost = []
    DocSwitch = []
    HumSwitch = []
    livesFell = []
    AudioVals = []
    AudioSliders = []
    StoreItemsBought = []

    for index, js in enumerate(json_files):
        with open(os.path.join(path_to_json, js)) as json_file:
            json_text = json.load(json_file)
            for row in json_text:
                if row['name'] == 'Doctor switch':
                    DocSwitch.append(DoctorSwitch(row['custom_params'], row['ts']))
                if row['name'] == 'Audio Slider':
                    AudioSliders.append(AudioSlider(row['custom_params'], row['ts']))
                if row['name'] == 'Audio value':
                    AudioVals.append(AudioValue(row['custom_params'], row['ts']))
                if row['name'] == 'Game Statistics':
                    gameStats.append(GameStatistics(row['custom_params'], row['ts']))
                if row['name'] == 'Human switch':
                    HumSwitch.append(HumanSwitch(row['custom_params'], row['ts']))
                if row['name'] == 'Life Lost':
                    livesLost.append(LifeLost(row['custom_params'], row['ts']))
                if row['name'] == 'Player fell':
                    livesFell.append(PlayerFell(row['custom_params'], row['ts']))
                if row['name'] == 'Store item bought':
                    StoreItemsBought.append(StoreItemBought(row['custom_params'], row['ts']))
        
    DocSwitch.sort(key=lambda x: x.ts)
    HumSwitch.sort(key=lambda x: x.ts)
    AudioSliders.sort(key=lambda x: x.ts)
    AudioVals.sort(key=lambda x: x.ts)
    livesLost.sort(key=lambda x: x.ts)
    livesFell.sort(key=lambda x: x.ts)
    StoreItemsBought.sort(key=lambda x: x.ts)
    gameStats.sort(key=lambda x: x.ts)

    workbook = xlsxwriter.Workbook('C:\\Users\\rushk\\Desktop\\CS books\\CSCI 526\\Analytics Script\\AnalyticsStats.xlsx')
    gameStatsWB = workbook.add_worksheet('Game Statistics')

    row = 0
    col = 1
    gameStatsWB.write(0,0, 'Timestamp')

    for i in sorted (gameStats[0].dict.keys()):
        gameStatsWB.write(row, col, i)
        col += 1

    row += 1
    for gs in gameStats:
        col = 0
        gameStatsWB.write(row, col, gs.ts)
        col += 1
        
        for i in sorted (gs.dict.keys()) : 
            gameStatsWB.write(row, col, gs.dict[i])
            col += 1
        
        row +=1

    
    DocSwitchWB = workbook.add_worksheet('Doctor Switch')

    row = 0
    col = 1
    DocSwitchWB.write(0,0, 'Timestamp')

    for i in sorted (DocSwitch[0].dict.keys()):
        DocSwitchWB.write(row, col, i)
        col += 1

    row += 1
    for gs in DocSwitch:
        col = 0
        DocSwitchWB.write(row, col, gs.ts)
        col += 1
        
        for i in sorted (gs.dict.keys()) : 
            DocSwitchWB.write(row, col, gs.dict[i])
            col += 1
        
        row +=1


    HumSwitchWB = workbook.add_worksheet('Human Switch')

    row = 0
    col = 1
    HumSwitchWB.write(0,0, 'Timestamp')

    for i in sorted (HumSwitch[0].dict.keys()):
        HumSwitchWB.write(row, col, i)
        col += 1

    row += 1
    for gs in HumSwitch:
        col = 0
        HumSwitchWB.write(row, col, gs.ts)
        col += 1
        
        for i in sorted (gs.dict.keys()) : 
            HumSwitchWB.write(row, col,  gs.dict[i])
            col += 1
        
        row +=1
    

    AudioValsWB = workbook.add_worksheet('Audio Values')

    row = 0
    col = 1
    AudioValsWB.write(0,0, 'Timestamp')

    for i in sorted (AudioVals[0].dict.keys()):
        AudioValsWB.write(row, col, i)
        col += 1

    row += 1
    for gs in AudioVals:
        col = 0
        AudioValsWB.write(row, col, gs.ts)
        col += 1
        
        for i in sorted (gs.dict.keys()) : 
            AudioValsWB.write(row, col, gs.dict[i])
            col += 1
        
        row +=1


    livesLostWB = workbook.add_worksheet('Life Lost')

    row = 0
    col = 1
    livesLostWB.write(0,0, 'Timestamp')

    for i in sorted (livesLost[0].dict.keys()):
        livesLostWB.write(row, col, i)
        col += 1

    row += 1
    for gs in livesLost:
        col = 0
        livesLostWB.write(row, col, gs.ts)
        col += 1
        
        for i in sorted (gs.dict.keys()) : 
            livesLostWB.write(row, col, gs.dict[i])
            col += 1
        
        row +=1


    StoreItemsBoughtWB = workbook.add_worksheet('Store Items')

    row = 0
    col = 1
    StoreItemsBoughtWB.write(0,0, 'Timestamp')

    for i in sorted (StoreItemsBought[0].dict.keys()):
        StoreItemsBoughtWB.write(row, col, i)
        col += 1

    row += 1
    for gs in StoreItemsBought:
        col = 0
        StoreItemsBoughtWB.write(row, col, gs.ts)
        col += 1
        
        for i in sorted (gs.dict.keys()) : 
            StoreItemsBoughtWB.write(row, col, gs.dict[i])
            col += 1
        
        row +=1


    AudioSlidersWB = workbook.add_worksheet('Audio Sliders')

    row = 0
    col = 1
    AudioSlidersWB.write(0,0, 'Timestamp')
    AudioSlidersWB.write(0,1, 'Count')

    row += 1
    for gs in AudioSliders:
        col = 0
        AudioSlidersWB.write(row, col, gs.ts)
        col += 1
        AudioSlidersWB.write(row, col, 1)
        
        row +=1


    livesFellWB = workbook.add_worksheet('Players Fell')

    row = 0
    col = 1
    livesFellWB.write(0,0, 'Timestamp')
    livesFellWB.write(0,1, 'Count')

    row += 1
    for gs in livesFell:
        col = 0
        livesFellWB.write(row, col, gs.ts)
        col += 1
        livesFellWB.write(row, col, 1)
        
        row +=1

    workbook.close()
