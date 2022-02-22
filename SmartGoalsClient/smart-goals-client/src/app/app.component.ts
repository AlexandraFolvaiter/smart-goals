import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ApplicationDetailsComponent } from './goals/application-details/application-details.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private readonly dialog: MatDialog) { }

  openDetailsDialog() {
    this.dialog.open(ApplicationDetailsComponent, {
      width: '600px',
      height: '510px',
    });
  }
}
