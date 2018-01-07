
class Time {

  constructor(){
    this.start = new Date("2020-01-01 00:00:00");
    this.time = this.start;
  }

  tick() {
    this.time.setDate(this.time.getDate() + 1);
    return this.time;
  }

  getTime(){
    return this.time;
  }

  getDay(){
    return this.time.getDate();
  }

  printDay(){
    switch(this.time.getDay()){
      case 1: return "Montag";
        break;
      case 2: return "Dienstag";
        break;
      case 3: return "Mittwoch";
        break;
      case 4: return "Donnerstag";
        break;
      case 5: return "Freitag";
        break;
      case 6: return "Samstag";
        break;
      case 7: return "Sonntag";
        break;
      default: return "";
        break;
    }
  }

  printMonth(){
    switch(this.time.getMonth()){
      case 0: return "Januar";
        break;
      case 1: return "Februar";
        break;
      case 2: return "März";
        break;
      case 3: return "April";
        break;
      case 4: return "Mai";
        break;
      case 5: return "Juni";
        break;
      case 6: return "Juli";
        break;
      case 7: return "August";
        break;
      case 8: return "September";
        break;
      case 9: return "Oktober";
        break;
      case 10: return "November";
        break;
      case 11: return "Dezember";
        break;
      default: return "";
        break;
    }
  }

  printYear(){
    return 1900 + this.time.getYear();
  }

  printDate(){
    return `${this.printDay()}, ${this.getDay()}. ${this.printMonth()} ${this.printYear()}`
  }

}

class Money {

  constructor(){
    this.value = 50000000;
  }

  get(){
    return this.value;
  }

  add(value){
    this.value = this.value + value;
    return this.value;
  }

  remove(value){
    if(this.value < value){
      throw "Not enough money";
    } else{
      this.value = this.value - value;
      return this.value;
    }
  }

  print(){
    return this.value.toLocaleString('de-DE') + " €";
  }

}

class Lobby {

}

class Plastic {
  
}
