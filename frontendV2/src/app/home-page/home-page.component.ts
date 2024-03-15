import {Component} from '@angular/core';
import {StateService} from '../state.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  toTranslate?: string;
  chosenLanguage?: string = "Afrikaans";
  fromLanguage?: string;
  fromLanguageCode?: string;
  languageCode?: string;

  constructor(public state: StateService) {
  }

  onTranlate() {
    this.findLanguageCode()
    var dto = {
      eventType: "ClientWantsToTranslate",
      messageToTranslate: this.toTranslate,
      toLanguage: this.languageCode,
      fromLanguage: this.fromLanguageCode,
    }
    this.state.ws.send(JSON.stringify(dto));
  }

  findLanguageCode() {
    for (let i = 0; i < this.state.languages!.length; i++) {
      if (this.state.languages![i] === this.chosenLanguage) {
        this.languageCode = this.state.code![i];
        break
      }
    }
    for (let i = 0; i < this.state.fromLanguages!.length; i++) {
      if (this.state.fromLanguages![i] === this.fromLanguage) {
        this.fromLanguageCode = this.state.code![i];
        break
      }
    }
  }

  sentBlobToWs(byteArray: Uint8Array) {
    var dto = {
      "eventType": "ClientWantsToTranslateAudio",
      "audioToTranslate": byteArray,
      "toLanguage": this.languageCode,
      "fromLanguage": this.fromLanguageCode,
    };
    this.state.ws.send(JSON.stringify(dto));
  }
  async onMicrophone() {
    try {
      this.findLanguageCode();
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
      const mediaRecorder = new MediaRecorder(stream);
      const audioChunks: BlobPart[] = [];

      mediaRecorder.addEventListener("dataavailable", (event) => {
        audioChunks.push(event.data);
      });

      mediaRecorder.addEventListener("stop", () => {
        const audioBlob = new Blob(audioChunks, { type: "audio/wav" });
        console.log("audioBlob: ", audioBlob);
        const reader = new FileReader();
        reader.onload = (event) => { // Changed to arrow function
          const base64String = event.target!.result as string;
          console.log(base64String)
          var dto = {
            "eventType": "ClientWantsToTranslateAudio",
            "audioToTranslate": base64String,
            "toLanguage": this.languageCode,
            "fromLanguage": this.fromLanguageCode,
          };
          this.state.ws.send(JSON.stringify(dto));
        };
        reader.readAsDataURL(audioBlob); // Changed to readAsDataURL
      });

      mediaRecorder.start();

      setTimeout(() => {
        mediaRecorder.stop();
      }, 2000);
    } catch (error) {
      console.error("Error accessing microphone:", error);
    }


    /*navigator.mediaDevices.getUserMedia({audio: true})
      .then(stream => {
        const mediaRecorder = new MediaRecorder(stream);
        mediaRecorder.start();
        const audioChunks: BlobPart[] | undefined = [];

        mediaRecorder.addEventListener("dataavailable", event => {
          audioChunks.push(event.data);
        });
        mediaRecorder.addEventListener("stop", () => {
          const audioBlob = new Blob(audioChunks);
          const audioUrl = URL.createObjectURL(audioBlob);
          const audio = new Audio(audioUrl);
          audio.play();
        });
        setTimeout(() => {
          mediaRecorder.stop();
        }, 5000);
      });*/
  }
}
