from flask import Flask, render_template

app = Flask(__name__)    
    
@app.route("/")
def index():
        return render_template("index.html")

@app.route("/aboutus")
def about_us():
    return render_template("AboutUs.html")

@app.route("/locations")
def locations():
    return render_template("Locations.html")

@app.route("/simulator")
def simulator():
    return render_template("Simulator.html")

@app.route("/sim_01")
def sim_01():
    return render_template("Sim_01.html")

@app.route("/sim_02")
def sim_02():
    return render_template("Sim_02.html")

@app.route("/sim_03")
def sim_03():
    return render_template("Sim_03.html")

@app.route("/sim_04")
def sim_04():
    return render_template("Sim_04.html")

@app.route("/sim_D")
def sim_D():
    return render_template("Sim_D.html")

@app.route("/sim_DL")
def sim_DL():
    return render_template("Sim_DL.html")

@app.route("/sim_DM")
def sim_DM():
    return render_template("Sim_DM.html")

@app.route("/sim_W")
def sim_W():
    return render_template("Sim_W.html")

@app.route("/sim_WL")
def sim_WL():
    return render_template("Sim_WL.html")

@app.route("/sim_WI")
def sim_WI():
    return render_template("Sim_WI.html")

@app.route("/sim_WLR")
def sim_WLR():
    return render_template("Sim_WLR.html")

@app.route("/sim_R")
def sim_R():
    return render_template("Sim_R.html")

@app.route("/sim_M")
def sim_M():
    return render_template("Sim_M.html")

@app.route("/sim_B")
def sim_B():
    return render_template("Sim_B.html")

@app.route("/howtouse")
def howtouse():
    return render_template("HowToUse.html")

@app.route("/contact")
def contact():
    return render_template("Contact.html")


app.run(debug=True)