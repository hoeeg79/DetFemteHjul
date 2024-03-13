import { Component } from '@angular/core';
import { StateService } from '../state.service';
import {interval} from "rxjs";

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  toTranslate?: string;
  chosenLanguage?: string;
  fromLanguage?: string;
  constructor(public state: StateService) {}

  onTranlate() {
    if (!this.fromLanguage){
      this.fromLanguage = "NoLanguageSelected";
    }
    let languageCode;
    console.log(this.chosenLanguage);
    for (let i = 0; i < this.state.languages!.length; i++) {
      console.log(this.state.languages![i]);
      if (this.state.languages![i] === this.chosenLanguage){
        languageCode = this.state.code![i];
        break
      }
    }
    var dto = {
      eventType: "ClientWantsToTranslate",
      messageToTranslate: this.toTranslate,
      chosenLanguage: languageCode,
      fromLanguage: this.fromLanguage,
    }
    this.state.ws.send(JSON.stringify(dto));
  }
}
