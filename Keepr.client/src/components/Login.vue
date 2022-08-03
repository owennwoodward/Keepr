<template>
  <span class="navbar-text">
    <button class="btn selectable text-success lighten-30 text-uppercase my-2 my-lg-0" @click="login"
      v-if="!user.isAuthenticated">
      <h3 class="text-dark">Login</h3>
    </button>

    <div class="dropdown my-2 my-lg-0 card bg-dark p-2" v-else>
      <div class="dropdown-toggle selectable" data-bs-toggle="dropdown" aria-expanded="false" id="authDropdown">
        <div class="mt-1" v-if="account.picture">
          <img :src="account.picture" alt="account photo" height="40" class="rounded" />
          <span class="mx-2  h5 text-light lighten-30 ">{{ account.name }}</span>
        </div>
      </div>
      <div class="dropdown-menu p-0 list-group w-100" aria-labelledby="authDropdown">
        <router-link :to="{ name: 'Account' }">
          <div class="list-group-item list-group-item-action hoverable">
            Manage Account
          </div>
        </router-link>
        <div class="list-group-item list-group-item-action hoverable text-danger" @click="logout">
          <i class="mdi mdi-logout"></i>
          logout
        </div>
        <router-link :to="{ name: 'Profile', params: { id: account.id } }">
          <div class="list-group-item list-group-item-action hoverable">
            Go to Profile
          </div>
        </router-link>
      </div>
    </div>
  </span>
</template>


<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { router } from "../router";
import { AuthService } from "../services/AuthService";
export default {
  setup() {
    return {
      profile: computed(() => AppState.profile),
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup();
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin });
      },
      takeToProfile() {
        Modal.getOrCreateInstance(document.getElementById('keep-modal')).hide()
        router.push({
          name: 'Profile',
          params: {
            id:
              AppState.profile.id
          }
        })
      }
    };
  },
};
</script>


<style lang="scss" scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}

.dropdown-menu.show {
  transform: scale(1);
}

.hoverable {
  cursor: pointer;
}
</style>
