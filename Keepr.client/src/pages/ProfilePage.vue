<template>
    <div class="container-fluid pb-5 ms-4">
        <div class="row">
            <div class="col d-flex mt-4 ms-4">
                <img :src="profile.picture" class="">
                <div>
                    <h1 class="pt-4 ms-5">{{ profile.name }}</h1>
                    <h2 class="ms-5">Vaults: {{ vaults.length }}</h2>
                    <h2 class="ms-5">Keeps: {{ keeps.length }}</h2>

                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid ms-4">
        <div class="row ms-4">
            <h2 class="">Vaults <span class="mdi mdi-plus selectable" v-if="profile.id == account.id"
                    @click="getVaultForm">
                </span></h2>
            <div class="col-4" v-for="v in vaults" :key="v.id">
                <Vault :vault="v" />
            </div>

        </div>

    </div>

    <div class="container-fluid p-4 ms-5 masonry-frame">
        <h2 class="">Keeps <span class="mdi mdi-plus selectable" v-if="profile.id == account.id"
                @click="getKeepForm"></span></h2>
        <div v-for="k in keeps" :key="k.id">
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
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
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
    components: { Keep }
}
</script>


<style lang="scss" scoped>
body {
    margin: 0;
    padding: 1rem;
}

.masonry-frame {
    columns: 6 200px;
    column-gap: 2rem;

    div {
        width: 150px;
        color: white;
        margin: 0 1rem 1rem 0;
        display: inline-block;
        width: 100%;
        text-align: center;
        font-family: system-ui;
        font-weight: 900;
        font-size: 2rem;
    }

    @for $i from 1 through 36 {
        div:nth-child(#{$i}) {
            $h: (random(400) + 100)+px;
            height: $h;
            line-height: $h;
        }
    }
}
</style>