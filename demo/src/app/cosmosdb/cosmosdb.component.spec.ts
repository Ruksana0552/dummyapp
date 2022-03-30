import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CosmosdbComponent } from './cosmosdb.component';

describe('CosmosdbComponent', () => {
  let component: CosmosdbComponent;
  let fixture: ComponentFixture<CosmosdbComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CosmosdbComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CosmosdbComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
