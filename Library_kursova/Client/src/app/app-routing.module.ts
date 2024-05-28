import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CatalogComponent } from './catalog/catalog.component';
import { MainComponent } from './main/main.component';
import { ProfileComponent } from './profile/profile.component';
import { BookmarksComponent } from './profile/bookmarks/bookmarks.component';
import { SettingsComponent } from './profile/settings/settings.component';
import { BorrowsComponent } from './profile/borrows/borrows.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { CreateAuthorComponent } from './admin/create-author/create-author.component';
import { AuthGuard } from './_guards/auth.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { SearchComponent } from './search/search.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { adminGuard } from './_guards/admin.guard';
import { CreatePublishingHouseComponent } from './admin/create-publishing-house/create-publishing-house.component';
import { CreateBookComponent } from './admin/create-book/create-book.component';

const appRoutes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
        children: [
          { path: 'bookmarks', component: BookmarksComponent },
          {
            path: 'settings',
            component: SettingsComponent,
            canDeactivate: [preventUnsavedChangesGuard],
          },
          { path: 'borrows', component: BorrowsComponent },
        ],
      },
      { path: 'create-author', component: CreateAuthorComponent },
      { path: 'create-house', component: CreatePublishingHouseComponent },
      { path: 'create-book', component: CreateBookComponent },
      {
        path: 'admin',
        component: AdminPanelComponent,
        canActivate: [adminGuard],
      },
    ],
  },
  { path: 'main', component: MainComponent },
  { path: 'catalog', component: CatalogComponent },
  { path: 'book/:id', component: BookDetailsComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: 'search', component: SearchComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule, RouterModule.forRoot(appRoutes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
