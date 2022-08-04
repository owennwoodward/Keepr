<template>
    <div class="container-fluid pb-5 pt-5 ">
        <div class="row ">
            <div class="col-2 d-flex justify-content-center">
                <img :src="profile.picture" class="rounded-pill">
            </div>
            <div class="col-10 ">
                <h1 class="">{{ profile.name }}</h1>
                <h2 class="">Vaults: {{ vaults.length }}</h2>
                <h2 class="">Keeps: {{ keeps.length }}</h2>

            </div>
        </div>
    </div>
    <div class="container-fluid ">
        <div class="row ms-4 pb-4 pt-4 ">
            <h1 class="ps-5 ">Vaults <span class=" mdi mdi-plus selectable " v-if="profile.id == account.id"
                    title="Create New Vault" @click="getVaultForm">
                </span></h1>
            <div class="col-3 ms-3 pt-5" v-for="v in vaults" :key="v.id" title="Go to Vault">
                <Vault :vault="v" />
            </div>

        </div>

    </div>


    <h1 class="ps-4 ms-5 ">Keeps <span class="mdi mdi-plus selectable" v-if="profile.id == account.id"
            title="Create New Keep" @click="getKeepForm"></span></h1>
    <div class="masonry-frame ">
        <div class="pt-5" v-for="k in keeps" :key="k.id" title="Go to Keep">
            <Keep :keep="k" />
        </div>
    </div>
</template>


<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import Keep from '../components/Keep.vue'
import { AppState } from '../AppState'
import { Modal } from 'bootstrap'

export default {
    setup() {
        const route = useRoute();
        onMounted(async () => {
            try {
                await profilesService.getMyProfile(route.params.id);
                await profilesService.getKeeps(route.params.id);
                await profilesService.getVaults(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        });
        return {
            profile: computed(() => AppState.profile),
            // keeps: computed(() => AppState.keeps),
            keeps: computed(() => AppState.profileKeeps),
            // vaults: computed(() => AppState.vaults),
            vaults: computed(() => AppState.profileVaults),
            account: computed(() => AppState.account),
            async getVaultForm() {
                try {

                    Modal.getOrCreateInstance(document.getElementById('createVault-modal')).show();
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message, "error");
                }
            },

            async getKeepForm() {
                try {
                    Modal.getOrCreateInstance(document.getElementById('createKeep-modal')).show();
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message, "error");
                }
            }
        };
    },
}
</script>


<style lang="scss" scoped>
.masonry-frame {
    columns: 4 200px;
    column-gap: 1rem;

    div {
        break-inside: avoid;
    }
}
</style>