import { CanDeactivateFn } from '@angular/router';
import { SettingsComponent } from '../profile/settings/settings.component';

export const preventUnsavedChangesGuard: CanDeactivateFn<SettingsComponent> = (component) => {
  if (component.updateUserForm?.dirty) {
    return confirm('Ви впевнені, що хочете вийти? Не збережені зміни будуть втрачені.')
  }
  
  return true;
};
